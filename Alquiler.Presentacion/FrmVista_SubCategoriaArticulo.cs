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
    public partial class FrmVista_SubCategoriaArticulo : Form
    {
        public FrmVista_SubCategoriaArticulo()
        {
            InitializeComponent();
        }

        private void FrmVista_SubCategoriaArticulo_Load(object sender, EventArgs e)
        {
            this.Listar();
        }
        private void Listar()
        {
            try
            {
                DgvListado.DataSource = NSubcategoria.Listar();
                this.Formato();
                
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
                DgvListado.DataSource = NSubcategoria.Buscar(TxtBuscar.Text);
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
            DgvListado.Columns[2].Width = 150;
            DgvListado.Columns[3].Width = 200;

            DgvListado.Columns[0].Width = 100;
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            this.buscar();
        }

        private void DgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Variables.IdSubCategoria = Convert.ToInt32(DgvListado.CurrentRow.Cells["ID"].Value);
            Variables.NombreSubCategoria = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
            this.Close();
        }
    }
}
