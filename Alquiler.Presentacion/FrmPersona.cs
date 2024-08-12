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
    public partial class FrmPersona : Form
    {
        private string CIAnt;
        public FrmPersona()
        {
            InitializeComponent();
        }

        private void FrmPersona_Load(object sender, EventArgs e)
        {
            this.Listar();
            this.CargarCiudad();
        }
        private void Listar()
        {
            try
            {
                DgvListado.DataSource = NPersona.Listar();
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
                DgvListado.DataSource = NPersona.Buscar(TxtBuscar.Text);
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
            TxtNombrePrimero.Clear();
            TxtApellidoPaterno.Clear();
            TxtNombreSegundo.Clear();
            TxtApellidoMaterno.Clear();
            TxtCI.Clear();
            TxtAlias.Clear();
            TxtTelefono.Clear();

            BtnInsertar.Visible = true;
            ErrorIcono.Clear();
            BtnActualiar.Visible = false;

            DgvListado.Columns[0].Visible = false;
            BtnAcivar.Visible = false;
            BtnDesactivar.Visible = false;
            BtnEliminar.Visible = false;

            ChkSeleccionar.Checked = false;

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

        private void validar_ci(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 33 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
            {
                MessageBox.Show("no inserte simbolos alfanumericos", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Handled = true;
                return;

            }
        }
        private void MensajeError(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistma de ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void MensajeOk(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistma de ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CargarCiudad()
        {
            try
            {
                CboCiudad.Items.Add("Santa Cruz");
                CboCiudad.Items.Add("Beni");
                CboCiudad.Items.Add("Cochabamba");
                CboCiudad.Items.Add("Chuquisaca");
                CboCiudad.Items.Add("La Paz");
                CboCiudad.Items.Add("Oruro");
                CboCiudad.Items.Add("Pando");
                CboCiudad.Items.Add("Potosi");
                CboCiudad.Items.Add("Tarija");

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
                if (TxtNombrePrimero.Text == string.Empty ||  TxtApellidoPaterno.Text == string.Empty || TxtCI.Text == string.Empty || CboCiudad.Text == string.Empty || TxtTelefono.Text == string.Empty)
                {
                    this.MensajeError("Falta ingresar alugnos datos, seran remarcados");
                    ErrorIcono.SetError(TxtNombrePrimero, "Ingrese Nombre");
                    
                    ErrorIcono.SetError(TxtApellidoPaterno, "Ingrese Apellido");
                    
                    ErrorIcono.SetError(TxtCI, "Ingrese Carnet I.");
                    ErrorIcono.SetError(CboCiudad, "Ingrese Ciudad");
                    ErrorIcono.SetError(TxtTelefono, "Ingrese Telefono");

                }
                else
                {
                    rpta = NPersona.Insertar(TxtNombrePrimero.Text.Trim(), TxtNombreSegundo.Text.Trim(), TxtApellidoPaterno.Text.Trim(), TxtApellidoMaterno.Text.Trim(), TxtAlias.Text.Trim(), TxtCI.Text.Trim(), CboCiudad.Text, TxtTelefono.Text.Trim());
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

        private void DgvListado_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
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
                this.CIAnt = Convert.ToString(DgvListado.CurrentRow.Cells["CI"].Value);
                string [] NombreCompleto = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value).Split(' ');
                
                TxtNombrePrimero.Text = NombreCompleto[0];
                TxtNombreSegundo.Text = NombreCompleto[1];
                string[] ApellidoCompleto = Convert.ToString(DgvListado.CurrentRow.Cells["Apellido"].Value).Split(' ');
                TxtApellidoPaterno.Text = ApellidoCompleto[0];
                TxtApellidoMaterno.Text = ApellidoCompleto[1];
                TxtCI.Text = Convert.ToString(DgvListado.CurrentRow.Cells["CI"].Value);
                TxtAlias.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Alias"].Value);

                CboCiudad.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Ciudad"].Value);
                TxtTelefono.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Telefono"].Value);

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
                if (TxtNombrePrimero.Text == string.Empty ||  TxtId.Text == string.Empty || TxtApellidoPaterno.Text == string.Empty || TxtCI.Text == string.Empty || CboCiudad.Text == string.Empty || TxtTelefono.Text == string.Empty)
                {
                    this.MensajeError("Falta ingresar alugnos datos, seran remarcados");
                    ErrorIcono.SetError(TxtNombrePrimero, "Ingrese Nombre");
                    ErrorIcono.SetError(TxtApellidoPaterno, "Ingrese Apellido");
                    ErrorIcono.SetError(TxtCI, "Ingrese Carnet I.");
                    ErrorIcono.SetError(CboCiudad, "Ingrese Ciudad");
                    ErrorIcono.SetError(TxtTelefono, "Ingrese Telefono");

                }
                else
                {
                    rpta = NPersona.Actualizar(Convert.ToInt32(TxtId.Text),CIAnt, TxtNombrePrimero.Text.Trim(), TxtNombreSegundo.Text.Trim(), TxtApellidoPaterno.Text.Trim(), TxtApellidoMaterno.Text.Trim(), TxtAlias.Text.Trim(), TxtCI.Text.Trim(), CboCiudad.Text, TxtTelefono.Text.Trim());
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
                            Rpta = NPersona.Eliminar(Codigo);

                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se Elimino el registro: " + Convert.ToString(row.Cells[2].Value) +" "+ Convert.ToString(row.Cells[3].Value) + " CI: " + Convert.ToString(row.Cells[4].Value));
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
                            Rpta = NPersona.Activar(Codigo);

                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se Activo el registro: " + Convert.ToString(row.Cells[2].Value) + " " + Convert.ToString(row.Cells[3].Value) + " CI: " + Convert.ToString(row.Cells[4].Value));
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
                            Rpta = NPersona.Desactivar(Codigo);

                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se desactivo el registro: " + Convert.ToString(row.Cells[2].Value) + " " + Convert.ToString(row.Cells[3].Value) + " CI: " + Convert.ToString(row.Cells[4].Value));
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

        private void TxtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar_numeros(sender, e);
        }

        private void TxtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar_letras(sender, e);
        }

        private void TxtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar_letras(sender, e);
        }

        private void TxtCI_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar_ci(sender, e);
        }

        private void DgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TxtNombreSegundo_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar_letras(sender, e);
        }

        private void TxtApellidoMaterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar_letras(sender, e);
        }

        private void TxtAlias_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar_letras(sender, e);
        }
    }
}
