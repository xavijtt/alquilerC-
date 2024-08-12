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
    public partial class FrmArticulo : Form
    {
        private string ModeloAnt;
        public FrmArticulo()
        {
            InitializeComponent();
            
        }

        private void FrmArticulo_Load(object sender, EventArgs e)
        {
            this.Listar();
            this.CargarMarca();
        }

        private void Listar()
        {
            try
            {
                DgvListado.DataSource = NArticulo.Listar();
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
                DgvListado.DataSource = NArticulo.Buscar(TxtBuscar.Text);
                this.Formato();
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
            
            DgvListado.Columns[2].Visible = false;
            DgvListado.Columns[3].Visible = false;
            DgvListado.Columns[1].Width = 50;
            DgvListado.Columns[5].Width = 100;
            
            DgvListado.Columns[0].Width = 100;
        }

        private void Limpiar()
        {
            TxtBuscar.Clear();
            TxtModelo.Clear();
            TxtNombreSubC.Clear();
            TxtId.Clear();
            TxtIdSubCategoria.Clear();

            BtnInsertar.Visible = true;
            ErrorIcono.Clear();
            BtnActualiar.Visible = false;

            DgvListado.Columns[0].Visible = false;
            BtnAcivar.Visible = false;
            BtnDesactivar.Visible = false;
            BtnEliminar.Visible = false;

            ChkSeleccionar.Checked = false;

        }

        private void TxtBuscarSubC_Click(object sender, EventArgs e)
        {
            FrmVista_SubCategoriaArticulo vista = new FrmVista_SubCategoriaArticulo();
            vista.ShowDialog();
            TxtIdSubCategoria.Text = Convert.ToString(Variables.IdSubCategoria);
            TxtNombreSubC.Text = Variables.NombreSubCategoria;
        }

        private void MensajeError(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistma de ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void MensajeOk(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistma de ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            this.buscar();
        }

        private void BtnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (TxtModelo.Text == string.Empty || TxtNombreSubC.Text == string.Empty || TxtIdSubCategoria.Text == string.Empty || CboMarca.Text == string.Empty)
                {
                    this.MensajeError("Falta ingresar alugnos datos, seran remarcados");
                    ErrorIcono.SetError(TxtModelo, "Ingrese Modelo");
                    ErrorIcono.SetError(TxtNombreSubC, "Ingrese SubCategoria");
                    ErrorIcono.SetError(CboMarca, "Ingrese Marca");

                }
                else
                {
                    rpta = NArticulo.Insertar(TxtModelo.Text.Trim(),Convert.ToInt32(TxtIdSubCategoria.Text) ,Convert.ToInt32(CboMarca.SelectedValue));
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
            this.Limpiar();
            TabGeneral.SelectedIndex = 0;
        }

        private void DgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DgvListado.Columns["Seleccionar"].Index && e.RowIndex >= 0)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)DgvListado.Rows[e.RowIndex].Cells["Seleccionar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void DgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Limpiar();
                BtnActualiar.Visible = true;
                BtnInsertar.Visible = false;
                TxtId.Text = Convert.ToString(DgvListado.CurrentRow.Cells["ID"].Value);
                TxtIdSubCategoria.Text = Convert.ToString(DgvListado.CurrentRow.Cells["idsubcategoria"].Value);
                TxtNombreSubC.Text = Convert.ToString(DgvListado.CurrentRow.Cells["SubCategoria"].Value);
                //this.ModeloAnt = Convert.ToString(DgvListado.CurrentRow.Cells["Modelo"].Value);
                TxtModelo.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Modelo"].Value);
                CboMarca.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Marca"].Value);

                TabGeneral.SelectedIndex = 1;
            }
            catch (Exception)
            {

                MessageBox.Show("Seleccione desde la celda nombre");
            }
        }

        private void BtnActualiar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (TxtId.Text == String.Empty || TxtModelo.Text == string.Empty || TxtNombreSubC.Text == string.Empty || TxtIdSubCategoria.Text == string.Empty || CboMarca.Text == string.Empty)
                {
                    this.MensajeError("Falta ingresar alugnos datos, seran remarcados");
                    ErrorIcono.SetError(TxtModelo, "Ingrese Nombre");

                }
                else
                {
                    rpta = NArticulo.Actualizar(Convert.ToInt32(TxtId.Text), Convert.ToInt32(TxtIdSubCategoria.Text), Convert.ToInt32(CboMarca.SelectedValue), TxtModelo.Text.Trim());
                    if (rpta.Equals("OK"))
                    {
                        this.MensajeOk("Se actualizo de forma correctar los datos");
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

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkSeleccionar.Checked)
            {
                DgvListado.Columns[0].Visible = true;
                BtnAcivar.Visible = true;
                BtnDesactivar.Visible = true;
                BtnEliminar.Visible = true;
                DgvListado.CellDoubleClick -= DgvListado_CellDoubleClick;
            }
            else
            {
                DgvListado.Columns[0].Visible = false;
                BtnAcivar.Visible = false;
                BtnDesactivar.Visible = false;
                BtnEliminar.Visible = false;
                DgvListado.CellDoubleClick += DgvListado_CellDoubleClick;
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
                            Rpta = NArticulo.Eliminar(Codigo);

                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se Elimino el registro: " + Convert.ToString(row.Cells[4].Value) );
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

        private void CargarMarca()
        {
            try
            {
                CboMarca.DataSource = NMarca.Seleccionar();
                CboMarca.ValueMember = "idmarca";
                CboMarca.DisplayMember = "nombre";

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
        }
            private void BtnAcivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente deseas activar al o los registros?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK)
                {
                    int Codigo;
                    string Rpta = "";
                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToInt32(row.Cells[1].Value);
                            Rpta = NArticulo.Activar(Codigo);

                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se Activo el registro: " + Convert.ToString(row.Cells[4].Value));
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

        private void BtnDesactivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente deseas desactivar al o los registros?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK)
                {
                    int Codigo;
                    string Rpta = "";
                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToInt32(row.Cells[1].Value);
                            Rpta = NArticulo.Desactivar(Codigo);

                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se desactivo el registro: " + Convert.ToString(row.Cells[4].Value));
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

        private void BtnReporte_Click(object sender, EventArgs e)
        {
            Reportes.ReporteArticulos Reporte = new Reportes.ReporteArticulos();
            Reporte.ShowDialog();
        }
    }
}
