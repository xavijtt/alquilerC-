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
    public partial class FrmUsuario : Form
    {
        private string NombreAnt;
        public FrmUsuario()
        {
            InitializeComponent();
        }

        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            this.Listar();
            this.CargarRol();

        }
        private void Listar()
        {
            try
            {
                DgvListado.DataSource = NUsuario.Listar();
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
                DgvListado.DataSource = NUsuario.Buscar(TxtBuscar.Text);
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
            DgvListado.Columns[1].Visible = false;
            
            DgvListado.Columns[2].Visible=false;
            DgvListado.Columns[3].Width = 100;

            DgvListado.Columns[0].Width = 100;
        }

        private void Limpiar()
        {
            TxtBuscar.Clear();
            TxtUsuario.Clear();
            TxtClave.Clear();
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

        private void CargarRol()
        {
            try
            {
                CboRol.DataSource = NRol.Seleccionar();
                CboRol.ValueMember = "idrol";
                CboRol.DisplayMember = "nombre";

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
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
                if (TxtUsuario.Text == string.Empty || TxtClave.Text == string.Empty || CboRol.Text == string.Empty)
                {
                    this.MensajeError("Falta ingresar alugnos datos, seran remarcados");
                    ErrorIcono.SetError(TxtUsuario, "Ingrese ususario");
                    ErrorIcono.SetError(TxtClave, "Ingrese clave");
                    ErrorIcono.SetError(CboRol, "Ingrese Rol");

                }
                else
                {
                    rpta = NUsuario.Insertar(TxtUsuario.Text.Trim(), TxtClave.Text.Trim() ,Convert.ToInt32(CboRol.SelectedValue));
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
                this.NombreAnt = Convert.ToString(DgvListado.CurrentRow.Cells["Usuario"].Value);
                TxtUsuario.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Usuario"].Value);
                
                CboRol.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Rol"].Value);

                TabGeneral.SelectedIndex = 1;
            }
            catch (Exception)
            {

                MessageBox.Show("Seleccione desde la celda nombre");
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
                            Rpta = NUsuario.Eliminar(Codigo);

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
                            Rpta = NUsuario.Activar(Codigo);

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
                            Rpta = NUsuario.Desactivar(Codigo);

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

        private void BtnActualiar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (TxtUsuario.Text == string.Empty || TxtClave.Text == string.Empty || TxtId.Text == String.Empty || CboRol.Text == string.Empty)
                    
                {
                    this.MensajeError("Falta ingresar alugnos datos, seran remarcados");
                    ErrorIcono.SetError(TxtUsuario, "Ingrese ususario");
                    ErrorIcono.SetError(TxtClave, "Ingrese clave");
                    ErrorIcono.SetError(CboRol, "Ingrese Rol");


                }
                else
                {
                    rpta = NUsuario.Actualizar(Convert.ToInt32(TxtId.Text), Convert.ToInt32(CboRol.SelectedValue), NombreAnt, TxtUsuario.Text.Trim(), TxtClave.Text.Trim());
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
    }
}
