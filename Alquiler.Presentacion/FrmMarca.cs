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
    public partial class FrmMarca : Form
    {
        private string NombreAnt;
        public FrmMarca()
        {
            InitializeComponent();
        }

        private void FrmMarca_Load(object sender, EventArgs e)
        {
            this.Listar();
        }

        private void Listar()
        {
            try
            {
                DgvListado.DataSource = NMarca.Listar();
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
                DgvListado.DataSource = NMarca.Buscar(TxtBuscar.Text);
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
            DgvListado.Columns[1].Width = 50;
            DgvListado.Columns[2].Width = 150;
            DgvListado.Columns[3].Width = 200;
            
            DgvListado.Columns[0].Width = 100;
        }

        private void Limpiar()
        {
            TxtBuscar.Clear();
            TxtNombre.Clear();
            TxtId.Clear();
           
            BtnInsertar.Visible = true;
            ErrorIcono.Clear();
            BtnActualiar.Visible = false;

            DgvListado.Columns[0].Visible = false;
            BtnAcivar.Visible = false;
            BtnDesactivar.Visible = false;
            BtnEliminar.Visible = false;

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

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            this.buscar();
        }

        private void BtnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (TxtNombre.Text == string.Empty)
                {
                    this.MensajeError("Falta ingresar alugnos datos, seran remarcados");
                    ErrorIcono.SetError(TxtNombre, "Ingrese Nombre");

                }
                else
                {
                    rpta = NMarca.Insertar(TxtNombre.Text.Trim());
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
                TxtId.Text = Convert.ToString( DgvListado.CurrentRow.Cells["ID"].Value);
                
                this.NombreAnt = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                TxtNombre.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                
                TabGeneral.SelectedIndex = 1;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Seleccione desde la celda nombre" + ex);
               
            }
        }

        private void BtnActualiar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (TxtNombre.Text == string.Empty || TxtId.Text == String.Empty)
                {
                    this.MensajeError("Falta ingresar alugnos datos, seran remarcados");
                    ErrorIcono.SetError(TxtNombre, "Ingrese Nombre");

                }
                else
                {
                    rpta = NMarca.Actualizar(Convert.ToInt32(TxtId.Text), NombreAnt, TxtNombre.Text.Trim());
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
                            Rpta = NMarca.Eliminar(Codigo);

                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se Elimino el registro: " + Convert.ToString(row.Cells[2].Value));
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
                            Rpta = NMarca.Activar(Codigo);

                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se Activo el registro: " + Convert.ToString(row.Cells[2].Value));
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
                            Rpta = NMarca.Desactivar(Codigo);

                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se desactivo el registro: " + Convert.ToString(row.Cells[2].Value));
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

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
