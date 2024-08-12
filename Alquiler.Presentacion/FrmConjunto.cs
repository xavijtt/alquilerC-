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

namespace Alquiler.Presentacion
{
    public partial class FrmConjunto : Form
    {
        private DataTable DtDetalle = new DataTable();
        public FrmConjunto()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
       
        private void FrmConjunto_Load(object sender, EventArgs e)
        {
            this.Listar();
            this.CrearTabla();
        }

        private void Listar()
        {
            try
            {
                DgvListado.DataSource = NConjunto.Listar();
               // this.Formato();
                this.Limpiar();
                LblTotal.Text = "Total registros; " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void Limpiar()
        {

           TxtNombre.Clear();
            TxtBuscarArticulo.Clear();
            TxtId.Clear();
            

            BtnInsertar.Visible = true;
            ErrorIcono.Clear();
            DtDetalle.Clear();
           


            DgvListado.Columns[0].Visible = false;
            
            BtnEliminar.Visible = false;
            ChkSeleccionar.Checked = false;

            

            DgvArticulos.Columns.Clear();

        }

        private void MensajeError(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistma de ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void MensajeOk(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistma de ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

      

        private void DgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DgvMostrarDetalle.DataSource = NConjunto.ListarDetalle(Convert.ToInt32(DgvListado.CurrentRow.Cells["ID"].Value));
                string NombreDetalle;
                NombreDetalle = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                LblNombreDetalle.Text = NombreDetalle;
                PanelMostrar.Visible = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void BtnCerrarDEtalle_Click(object sender, EventArgs e)
        {
            PanelMostrar.Visible = false;
        }

        private void AgregarDetalle(int IdArticulo, string Nombre, string SubCategoria)
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



                this.DtDetalle.Rows.Add(Fila);


            }
        }

        private void FormatoArticulos()
        {
            DgvArticulos.Columns[0].Visible = false;
            DgvArticulos.Columns[1].Visible = false;
            DgvArticulos.Columns[2].Visible = false;
            DgvArticulos.Columns[3].Visible = true;





        }

        private void CrearTabla()
        {
            this.DtDetalle.Columns.Add("idarticulo", System.Type.GetType("System.Int32"));
            this.DtDetalle.Columns.Add("articulo", System.Type.GetType("System.String"));
            this.DtDetalle.Columns.Add("subcategoria", System.Type.GetType("System.String"));
            this.DtDetalle.Columns.Add("cantidad", System.Type.GetType("System.Int32"));

            DgvDetalle.DataSource = this.DtDetalle;

            DgvDetalle.Columns[0].Visible = true;
            DgvDetalle.Columns[1].HeaderText = "Articulo";
            DgvDetalle.Columns[1].Width = 100;
            DgvDetalle.Columns[2].HeaderText = "SubCategoria";
            DgvDetalle.Columns[2].Width = 100;
            DgvDetalle.Columns[3].HeaderText = "Cantidad";
            DgvDetalle.Columns[3].Width = 70;


            DgvDetalle.Columns[1].ReadOnly = true;


        }
      

        private void DgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DgvListado.Columns["Seleccionar"].Index && e.RowIndex >= 0)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)DgvListado.Rows[e.RowIndex].Cells["Seleccionar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void BtnVerArticulos_Click(object sender, EventArgs e)
        {
            PanelArticulos.Visible = true;
        }

        private void BtnCerrarArticulo_Click(object sender, EventArgs e)
        {
            PanelArticulos.Visible = false;
        }

        private void DgvArticulos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int IdArticulo;
            string Nombre;
            string SubCategoria;
            IdArticulo = Convert.ToInt32(DgvArticulos.CurrentRow.Cells["ID"].Value);
            Nombre = Convert.ToString(DgvArticulos.CurrentRow.Cells["Modelo"].Value);
            SubCategoria = Convert.ToString(DgvArticulos.CurrentRow.Cells["SubCategoria"].Value);
            this.AgregarDetalle(IdArticulo, Nombre, SubCategoria);

            PanelArticulos.Visible = false;
        }

        private void TxtBuscarArticulo_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void BtnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (DtDetalle.Rows.Count == 0)
                {
                    this.MensajeError("Falta ingresar alugnos datos, seran remarcados");
                    ErrorIcono.SetError(DgvDetalle, "Ingrese al menos un detalle");
                    ErrorIcono.SetError(TxtNombre, "Ingrese el nombre del set");
                    

                }
                else
                {


                    rpta = NConjunto.Insertar(Convert.ToString(TxtNombre.Text), DtDetalle);
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

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            TabGeneral.SelectedIndex = 0;
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

        private void validar_letras(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
            {
                MessageBox.Show("inserte letras", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Handled = true;
                return;

            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                DgvListado.DataSource = NConjunto.Buscar(TxtBuscar.Text);
              //  this.Formato();
                LblTotal.Text = "Total registros; " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
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

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
           


            if (ChkSeleccionar.Checked)
            {
                DgvListado.Columns[0].Visible = true;
              //  BtnAcivar.Visible = true;
            //    BtnDesactivar.Visible = true;
                BtnEliminar.Visible = true;
                DgvListado.CellDoubleClick -= DgvListado_CellDoubleClick;
            }
            else
            {
                DgvListado.Columns[0].Visible = false;
             //   BtnAcivar.Visible = false;
              //  BtnDesactivar.Visible = false;
                BtnEliminar.Visible = false;
                DgvListado.CellDoubleClick += DgvListado_CellDoubleClick;
            }
        }

        private void BtnCerrarDEtalle_Click_1(object sender, EventArgs e)
        {
            PanelMostrar.Visible = false;
        }

        private void TxtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 33 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
            {
                MessageBox.Show("no inserte simbolos alfanumericos", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Handled = true;
                return;

            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente deseas eliminar al o los registros?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK)
                {
                    int Codigo;
                    string Rpta = "";
                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToInt32(row.Cells[1].Value);
                            Rpta = NConjunto.Eliminar(Codigo);

                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se Elimino el registro: " + Convert.ToString(row.Cells[1].Value) + " " + Convert.ToString(row.Cells[2].Value));
                            }
                            else
                            {
                                this.MensajeError(Rpta);
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
    }
}
