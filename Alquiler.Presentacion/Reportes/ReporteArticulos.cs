using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alquiler.Presentacion.Reportes
{
    public partial class ReporteArticulos : Form
    {
        public ReporteArticulos()
        {
            InitializeComponent();
        }

        private void ReporteArticulos_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'dsSistema.articulo_listar' Puede moverla o quitarla según sea necesario.
            this.articulo_listarTableAdapter.Fill(this.dsSistema.articulo_listar);
            // TODO: esta línea de código carga datos en la tabla 'dsSistema.articulo_listar' Puede moverla o quitarla según sea necesario.



            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
