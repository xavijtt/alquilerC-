using Alquiler.Negocio;
using Alquiler.Presentacion.Reportes;
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
    public partial class FrmAlquiler : Form
    {
        private DataTable DtDetalle = new DataTable();
        public FrmAlquiler()
        {
            InitializeComponent();
        }


        private void FrmAlquiler_Load(object sender, EventArgs e)
        {
            this.Listar();
            this.CrearTabla();
            this.ChkBuscar.Checked = true;
        }
        private void buscar_fecha()
        {
            try
            {
                DgvListado.DataSource = NAlquiler.Buscar_Fecha(Convert.ToDateTime(fechainicial.Text), Convert.ToDateTime(fechafinal.Text));
                this.Formato();
                this.Limpiar();
                LblTotal.Text = "Total registros; " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }


        private void Formato()
        {
            DgvListado.Columns[0].Visible = false;
            DgvListado.Columns[1].Width = 50;
            DgvListado.Columns[2].Visible = false;
            DgvListado.Columns[3].Width = 200;

            DgvListado.Columns[0].Width = 100;
        }

        private void Listar()
        {
            try
            {
               DgvListado.DataSource = NAlquiler.Listar();
                this.Formato();
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
            
            TxtCi.Clear();
            TxtCliente.Clear();
        
            TxtBuscarCliente.Clear();
            TxtBuscarArticulo.Clear();
            TxtId.Clear();
            dateTimePicker1.ResetText();
            
            BtnInsertar.Visible = true;
            ErrorIcono.Clear();
            DtDetalle.Clear();
            TxtTotal.ResetText();
          

            
            BtnAnular.Visible = false;
            ChkSeleccionar.Checked = false;

            DgvPersonas.Columns.Clear();
            
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
                DgvMostrarDetalle.DataSource = NAlquiler.ListarDetalle(Convert.ToInt32(DgvListado.CurrentRow.Cells["ID"].Value));
                decimal Total;
                Total = Convert.ToDecimal(DgvListado.CurrentRow.Cells["Monto"].Value);
                TxtTotalDe.Text = Total.ToString();
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

        private void AgregarDetalle(int IdArticulo, string Nombre, string SubCategoria, int cantidad = 1)
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
                Fila["cantidad"] = cantidad;



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


        private void FormatoClientes()
        {
            DgvArticulos.Columns[0].Visible = false;
            DgvArticulos.Columns[1].Visible = false;
            DgvArticulos.Columns[2].Visible = false;
            DgvArticulos.Columns[3].Visible = true;





        }


        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkSeleccionar.Checked)
            {
                DgvListado.Columns[0].Visible = true;

                BtnAnular.Visible = true;
                BtnFinalizar.Visible = true;
                DgvListado.CellDoubleClick -= DgvListado_CellDoubleClick;
            }
            else
            {
                DgvListado.Columns[0].Visible = false;

                BtnAnular.Visible = false;
                BtnFinalizar.Visible = false;
                DgvListado.CellDoubleClick += DgvListado_CellDoubleClick;
            }
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

        private void BtnAnular_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente deseas anular al o los registros?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK)
                {
                    int Codigo;
                    string Rpta = "";
                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            if (Convert.ToString(row.Cells[8].Value) == "ALQUILER")
                            {
                                Codigo = Convert.ToInt32(row.Cells[1].Value);
                                Rpta = NAlquiler.Anular(Codigo);

                                if (Rpta.Equals("OK"))
                                {
                                    this.MensajeOk("Se Anulo el registro: " + Convert.ToString(row.Cells[2].Value));
                                }
                                else
                                {
                                    this.MensajeError(Rpta);
                                }
                            }
                            else
                            {
                                this.MensajeError("El registro: " + Convert.ToString(row.Cells[2].Value) + " se encuentra anulado o finalizado");
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

        private void BtnDevuelto_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente deseas finalizar el o los alquileres?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK)
                {
                    int Codigo;
                    string Rpta = "";
                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            if (Convert.ToString(row.Cells[8].Value) == "ALQUILER")
                            {
                                Codigo = Convert.ToInt32(row.Cells[1].Value);
                                Rpta = NAlquiler.Finalizar(Codigo);

                                if (Rpta.Equals("OK"))
                                {
                                    this.MensajeOk("Se finalizo el alquiler: " + Convert.ToString(row.Cells[2].Value));
                                }
                                else
                                {
                                    this.MensajeError(Rpta);
                                }

                                
                            }

                            else
                            {
                                this.MensajeError("El registro: " + Convert.ToString(row.Cells[2].Value) + " se encuentra finalizado o anulado");
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

        private void PanelArticulos_Paint(object sender, PaintEventArgs e)
        {

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
            this.AgregarDetalle(IdArticulo,Nombre, SubCategoria);
            
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

        private void DgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
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
                    ErrorIcono.SetError(TxtCi, "Ingrese el ci del cliente");
                    ErrorIcono.SetError(TxtCliente, "Ingrese el cliente");
                    ErrorIcono.SetError(TxtTotal, "Ingrese el monto");

                }
                else
                {
                  

                    rpta = NAlquiler.Insertar(Convert.ToInt32(TxtIdCLliente.Text),Convert.ToDateTime( dateTimePicker1.Value) ,Convert.ToDecimal(TxtTotal.Text), DtDetalle);
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

        private void TxtVerCliente_Click(object sender, EventArgs e)
        {
            PanelCliente.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PanelCliente.Visible = false;
        }

        private void BtnFiltrarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                DgvPersonas.DataSource = NPersona.Buscar(TxtBuscarCliente.Text.Trim());
                //this.FormatoArticulos();
                LblTotalClientes.Text = "Total Registros " + Convert.ToString(DgvPersonas.Rows.Count);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void DgvPersonas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int IdPersona;
            string Nombre;
            string Ci;
           
            IdPersona = Convert.ToInt32(DgvPersonas.CurrentRow.Cells["ID"].Value);
            Nombre = Convert.ToString(DgvPersonas.CurrentRow.Cells["Nombre"].Value) + " " + Convert.ToString(DgvPersonas.CurrentRow.Cells["Apellido"].Value);
            Ci = Convert.ToString(DgvPersonas.CurrentRow.Cells["CI"].Value);

            TxtIdCLliente.Text = Convert.ToString( IdPersona);

            TxtCliente.Text = Nombre;
            TxtCi.Text = Ci;
            PanelCliente.Visible = false;
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            TabGeneral.SelectedIndex = 0;
        }

        private void DgvListado_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            
              

                
        }

        private void DgvDetalle_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

           
        }

        private void DgvDetalle_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void DgvDetalle_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int CantidadArticulo = Convert.ToInt32(DgvDetalle.CurrentRow.Cells["idarticulo"].Value);
                DataTable Tabla = new DataTable();
                Tabla = NAlquiler.ValidarCantidad(CantidadArticulo);

                if (Convert.ToInt32(DgvDetalle.CurrentRow.Cells["cantidad"].Value) >=0)
                {
                    if (Convert.ToInt32(Tabla.Rows[0][1]) < Convert.ToInt32(DgvDetalle.CurrentRow.Cells["cantidad"].Value))
                    {
                        MessageBox.Show("Cantidad no admitida", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("inserte numeros enteros positivos", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                    
                
                

                

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void TxtTotal_Validated(object sender, EventArgs e)
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

        private void validar_letras(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
            {
                MessageBox.Show("inserte letras", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Handled = true;
                return;

            }
        }

        
        private void TxtTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar_numeros(sender, e);
        }




        private void DgvListado_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void DgvDetalle_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnComprobanteAlquiler_Click(object sender, EventArgs e)
        {
            try
            {
                Variables.IdAlquiler = Convert.ToInt32(DgvListado.CurrentRow.Cells["ID"].Value);
                ReporteAlquiler reporteAlquiler = new ReporteAlquiler();
                reporteAlquiler.ShowDialog();
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                DgvListado.DataSource = NAlquiler.Buscar(TxtBuscar.Text);
                this.Formato();
                this.Limpiar();
                LblTotal.Text = "Total registros; " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void BtnBuscarFecha_Click(object sender, EventArgs e)
        {
            this.buscar_fecha();
        }

        private void ChkBuscar_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkBuscar.Checked)
            {
                TxtBuscar.Enabled = false;
                BtnBuscar.Enabled = false;
                BtnBuscarFecha.Enabled = true;
                fechafinal.Enabled = true;
                fechainicial.Enabled = true;
                BtnReset.Enabled = true;

            }
            else
            {
                

                BtnBuscarFecha.Enabled = false;
                fechafinal.Enabled = false;
                fechainicial.Enabled = false;
                TxtBuscar.Enabled = true;
                BtnBuscar.Enabled = true;
                BtnReset.Enabled = false;
            }
        }


      

        private void buscar_estado(CheckBox valor,bool alquiler,bool finalizado,bool anulado,string busqueda)
        {
            if (valor.Checked)
            {
                TxtBuscar.Enabled = false;
                BtnBuscar.Enabled = false;
                BtnBuscarFecha.Enabled = false;
                BtnReset.Enabled=false;
                fechafinal.Enabled = false;
                fechainicial.Enabled = false;
                ChkBuscar.Enabled = false;

                ChkAlquiler.Enabled = alquiler;
                ChkFinalizado.Enabled = finalizado;
                ChkAnulado.Enabled = anulado;


                try
                {
                    DgvListado.DataSource = NAlquiler.Buscar_Estado(busqueda);
                    this.Formato();
                    this.Limpiar();
                    LblTotal.Text = "Total registros; " + Convert.ToString(DgvListado.Rows.Count);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + ex.StackTrace);
                }

            }
            else
            {




                ChkBuscar.Enabled = true;

                if (alquiler==true)
                {
                    ChkAlquiler.Enabled = alquiler;
                    ChkFinalizado.Enabled = !finalizado;
                    ChkAnulado.Enabled = !anulado;
                }
                else if (finalizado==true)
                {
                    ChkAlquiler.Enabled = !alquiler;
                    ChkFinalizado.Enabled = finalizado;
                    ChkAnulado.Enabled = !anulado;
                }

                else if (anulado==true)
                {
                    ChkAlquiler.Enabled = !alquiler;
                    ChkFinalizado.Enabled = !finalizado;
                    ChkAnulado.Enabled = anulado;
                }

                this.Listar();

                if (ChkBuscar.Checked)
                {
                    TxtBuscar.Enabled = false;
                    BtnBuscar.Enabled = false;
                    BtnBuscarFecha.Enabled = true;
                    fechafinal.Enabled = true;
                    fechainicial.Enabled = true;
                    BtnReset.Enabled = true;

                }
                else
                {
                    TxtBuscar.Enabled = true;
                    BtnBuscar.Enabled = true;
                    BtnBuscarFecha.Enabled = false;
                    fechafinal.Enabled = false;
                    fechainicial.Enabled = false;
                    BtnReset.Enabled = false;
                }
            }
        }
       


        private void ChkAlquiler_CheckedChanged(object sender, EventArgs e)
        {


            buscar_estado(ChkAlquiler, true, false, false, "ALQUILER");
            

            
        }

        private void ChkFinalizado_CheckedChanged(object sender, EventArgs e)
        {
            buscar_estado(ChkFinalizado, false, true, false, "FINALIZADO");
            
        }

        private void ChkAnulado_CheckedChanged(object sender, EventArgs e)
        {
            buscar_estado(ChkAnulado, false, false, true, "ANULADO");
            
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
           
        }

        private void BtnSet_Click(object sender, EventArgs e)
        {
            PanelConjuntos.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PanelConjuntos.Visible = false;
        }

        private void BtnBuscarConjuntos_Click(object sender, EventArgs e)
        {
            try
            {
                DgvConjuntos.DataSource = NConjunto.Buscar(TxtBuscarConjuntos.Text.Trim());
                //this.FormatoArticulos();
                LblTotalConjuntos.Text = "Total Registros " + Convert.ToString(DgvConjuntos.Rows.Count);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void DgvConjuntos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataTable lista = NConjunto.ListarDetalle(Convert.ToInt32(DgvConjuntos.CurrentRow.Cells["ID"].Value));
                // decimal Total;
                //   Total = Convert.ToDecimal(DgvListado.CurrentRow.Cells["Monto"].Value);
                //  TxtTotalDe.Text = Total.ToString();

                
                DataTable Tabla = new DataTable();
                


                foreach (DataRow row in lista.Rows)
                {
                    Tabla = NAlquiler.ValidarCantidad(Convert.ToInt32(row["ID"]));
                    if (Convert.ToInt32(Tabla.Rows[0][1]) >= Convert.ToInt32(row["CANTIDAD"]))

                    {
                       
                        this.AgregarDetalle(Convert.ToInt32(row["ID"]), row["ARTICULO"].ToString(), row["Categoria"].ToString(), Convert.ToInt32(row["CANTIDAD"]));
                    }
                    else if (Convert.ToInt32(Tabla.Rows[0][1]) <= Convert.ToInt32(row["CANTIDAD"]))
                    {
                         //int cantidad_disp = Convert.ToInt32(Tabla.Rows[0][1]) - Convert.ToInt32(row["CANTIDAD"]);
                        this.AgregarDetalle(Convert.ToInt32(row["ID"]), row["ARTICULO"].ToString(), row["Categoria"].ToString(), Convert.ToInt32(Tabla.Rows[0][1]));
                    }
                    
                }
                PanelMostrar.Visible = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


          //  int IdArticulo;
           // string Nombre;
           // string SubCategoria;
          //  IdArticulo = Convert.ToInt32(DgvArticulos.CurrentRow.Cells["ID"].Value);
          //  Nombre = Convert.ToString(DgvArticulos.CurrentRow.Cells["Modelo"].Value);
          //  SubCategoria = Convert.ToString(DgvArticulos.CurrentRow.Cells["SubCategoria"].Value);
           // this.AgregarDetalle(IdArticulo, Nombre, SubCategoria);

            PanelArticulos.Visible = false;
        }

        private void DgvMostrarDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                DgvListado.DataSource = NAlquiler.Buscar(TxtBuscar.Text);
                //this.Formato();
                LblTotal.Text = "Total registros; " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
    }
}
