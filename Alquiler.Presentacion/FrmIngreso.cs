using Alquiler.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Alquiler.Negocio;

namespace Alquiler.Presentacion
{
    public partial class FrmIngreso : Form
    {   
        private DataTable DtDetalle = new DataTable();
        public FrmIngreso()
        {
            InitializeComponent();
        }

        private void FrmIngreso_Load(object sender, EventArgs e)
        {
            this.Listar();
            this.CrearTabla();
            
        }

        private void Listar()
        {
            try
            {
                DgvListado.DataSource = NIngreso.Listar();
                this.Formato();
                this.Limpiar();
                LblTotal.Text = "Total registros; " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void buscar()
        {
            try
            {
                DgvListado.DataSource = NIngreso.Buscar(Convert.ToDateTime(fechainicial.Text),Convert.ToDateTime(fechafinal.Text));
                this.Formato();
                this.Limpiar();
                LblTotal.Text = "Total registros; " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void FormatoArticulos()
        {
            DgvArticulos.Columns[0].Visible = false;
            DgvArticulos.Columns[1].Visible = false;
            DgvArticulos.Columns[2].Visible = false;
            DgvArticulos.Columns[3].Visible = true;

           



        }
        private void Formato()
        {
            DgvListado.Columns[0].Visible = false;
            DgvListado.Columns[2].Visible = false;
            DgvListado.Columns[1].Width = 50;
            
            DgvListado.Columns[3].Width = 100;
            DgvListado.Columns[4].Width = 150;
            DgvListado.Columns[5].Width = 150;

            DgvListado.Columns[0].Width = 100;
        }

        private void Limpiar()
        {
            
            TxtBuscarAr.Clear();
            TxtId.Clear();

            BtnInsertar.Visible = true;
            ErrorIcono.Clear();
            DtDetalle.Clear();
            TxtTotal.Text = "0.00";

            DgvListado.Columns[0].Visible = false;

            BtnAnular.Visible = false;
            ChkSeleccionar.Checked = false;
        }

        private void MensajeError(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistma de ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void MensajeOk(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistma de ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CrearTabla()
        {
            this.DtDetalle.Columns.Add("idarticulo", System.Type.GetType("System.Int32"));
            this.DtDetalle.Columns.Add("articulo", System.Type.GetType("System.String"));
            this.DtDetalle.Columns.Add("subcategoria", System.Type.GetType("System.String"));
            this.DtDetalle.Columns.Add("cantidad", System.Type.GetType("System.Int32"));
            this.DtDetalle.Columns.Add("precio", System.Type.GetType("System.Decimal"));
            this.DtDetalle.Columns.Add("importe", System.Type.GetType("System.Decimal"));
            DgvDetalle.DataSource=this.DtDetalle;

            DgvDetalle.Columns[0].Visible = false;
            DgvDetalle.Columns[1].HeaderText = "Articulo";
            DgvDetalle.Columns[1].Width = 100;
            DgvDetalle.Columns[2].HeaderText = "SubCategoria";
            DgvDetalle.Columns[2].Width = 100;
            DgvDetalle.Columns[3].HeaderText = "Cantidad";
            DgvDetalle.Columns[3].Width = 70;
            DgvDetalle.Columns[4].HeaderText = "Precio";
            DgvDetalle.Columns[4].Width = 70;
            DgvDetalle.Columns[5].HeaderText = "Importe";
            DgvDetalle.Columns[5].Width = 70;

            DgvDetalle.Columns[1].ReadOnly = true;


        }

        private void BtnVerArticulos_Click(object sender, EventArgs e)
        {
            PanelArticulos.Visible = true;
        }

        private void BtnCerrarArticulo_Click(object sender, EventArgs e)
        {
            PanelArticulos.Visible = false;
        }

        private void BtnFiltrarArticulos_Click(object sender, EventArgs e)
        {
            try
            {
                DgvArticulos.DataSource = NArticulo.Buscar(TxtBuscarArticulo.Text.Trim());
                this.FormatoArticulos();
                LblTotalArticulos.Text = "Total Registros " + Convert.ToString(DgvArticulos.Rows.Count);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void CalcularTotales()
        {
            decimal Total = 0;

            if(DgvDetalle.Rows.Count == 0)
            { 

                Total = 0;
            }
            else
            {
                foreach (DataRow FilaTemp in DtDetalle.Rows)
                {
                    Total = Total + Convert.ToDecimal(FilaTemp["importe"]);
                }
            }
            
            TxtTotal.Text = Total.ToString("#0.00#");
        }


        private void AgregarDetalle(int IdArticulo,string Nombre,string SubCategoria)
        {
            bool Agregar = true;
            foreach (DataRow FilaTemp in DtDetalle.Rows)
            {
                if (Convert.ToInt32(FilaTemp["idarticulo"]) == IdArticulo)
                {
                    Agregar = false;
                    this.MensajeError("El articulo ya ha sido agregado");
                }
            }
            if (Agregar)
            {
                DataRow Fila = DtDetalle.NewRow();
                Fila["idarticulo"] = IdArticulo;
                Fila["articulo"] = Nombre;
                Fila["subcategoria"] = SubCategoria;
                Fila["cantidad"] = 1;
                Fila["precio"] = 0;
                Fila["importe"] = 0;
               
                this.DtDetalle.Rows.Add(Fila);
                
                this.CalcularTotales();
            }

            


        }

        private void DgvArticulos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int IdArticulo;
            string Nombre;
            string SubCategoria;
            IdArticulo = Convert.ToInt32(DgvArticulos.CurrentRow.Cells["ID"].Value);
            Nombre = Convert.ToString(DgvArticulos.CurrentRow.Cells["Modelo"].Value);
            SubCategoria = Convert.ToString(DgvArticulos.CurrentRow.Cells["SubCategoria"].Value);
            this.AgregarDetalle(IdArticulo, Nombre,SubCategoria);
            PanelArticulos.Visible = false;
        }

        private void DgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataRow Fila = (DataRow)DtDetalle.Rows[e.RowIndex];
            decimal Precio;
            int Cantidad;
            if (Fila["Precio"] == null || Fila["Precio"] == DBNull.Value || String.IsNullOrWhiteSpace(Fila["Precio"].ToString()))
            {
                 Fila["Precio"] = 0;
                 
            }
            if (Fila["cantidad"] == null || Fila["cantidad"] == DBNull.Value || String.IsNullOrWhiteSpace(Fila["cantidad"].ToString()))
            {
                Fila["cantidad"] = 0;

            }


            Precio = Convert.ToDecimal(Fila["Precio"]);
            Cantidad = Convert.ToInt32(Fila["cantidad"]);
            Fila["importe"] = Precio * Cantidad;
            this.CalcularTotales();
        }

       

        private void DgvDetalle_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            this.CalcularTotales();
        }

        private void BtnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (DtDetalle.Rows.Count==0)
                {
                    this.MensajeError("Falta ingresar alugnos datos, seran remarcados");
                    ErrorIcono.SetError(DgvDetalle, "Ingrese al menos un detalle");

                }
                else
                {
                    

                    rpta = NIngreso.Insertar(Variables.IdUsuario,Convert.ToDecimal(TxtTotal.Text),DtDetalle);
                    if (rpta.Equals("OK"))
                    {
                        this.MensajeOk("Se inserto de forma correctar los datos");
                        this.Limpiar();
                        this.Listar();
                    }
                    else
                    {
                        this.MensajeError(rpta);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void DgvMostrarDetalle_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void BtnCerrarDEtalle_Click(object sender, EventArgs e)
        {
            PanelMostrar.Visible = false;
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Limpiar();
            TabGeneral.SelectedIndex=0; 
        }

        private void DgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DgvMostrarDetalle.DataSource = NIngreso.ListarDetalle(Convert.ToInt32(DgvListado.CurrentRow.Cells["ID"].Value));
                decimal Total;
                Total = Convert.ToDecimal(DgvListado.CurrentRow.Cells["Total"].Value);
                TxtTotalDe.Text = Total.ToString();
                PanelMostrar.Visible = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void DgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DgvListado.Columns["Seleccionar"].Index && e.RowIndex >= 0)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)DgvListado.Rows[e.RowIndex].Cells["Seleccionar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkSeleccionar.Checked)
            {
                DgvListado.Columns[0].Visible = true;

                BtnAnular.Visible = true;
                DgvListado.CellDoubleClick -= DgvListado_CellDoubleClick;
            }
            else
            {
                DgvListado.Columns[0].Visible = false;
               
                BtnAnular.Visible = false;
                DgvListado.CellDoubleClick += DgvListado_CellDoubleClick;
            }
        }

        private void BtnAnular_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente deseas anulas al o los registros?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK)
                {
                    int Codigo;
                    string Rpta = "";
                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            if (Convert.ToString(row.Cells[6].Value) == "ACEPTADO")
                            {
                                Codigo = Convert.ToInt32(row.Cells[1].Value);
                                Rpta = NIngreso.Anular(Codigo);

                                if (Rpta.Equals("OK"))
                                {
                                    this.MensajeOk("Se Anulo el registro: " + Convert.ToString(row.Cells[1].Value));
                                }
                                else
                                {
                                    this.MensajeError(Rpta);
                                }
                            }
                            else
                            {
                                this.MensajeError("El registro: " + Convert.ToString(row.Cells[1].Value) + " se encuentra anulado");
                            }
                            }
                    }
                    this.Listar();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void DgvMostrarDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            this.buscar();
        }

        private void DgvArticulos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void validar_numeros(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("inserte numeros enteros positivos", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Handled = true;
                return;

            }
        }
        private void DgvDetalle_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

        }

        private void DgvDetalle_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void DgvDetalle_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }
        

        private void DgvDetalle_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void BtnRestaurar_Click(object sender, EventArgs e)
        {
            this.Listar();
        }
    }
}
