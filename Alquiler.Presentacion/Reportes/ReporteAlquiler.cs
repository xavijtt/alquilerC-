using Alquiler.Presentacion.Properties;
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
    public partial class ReporteAlquiler : Form
    {
        public ReporteAlquiler()
        {
            InitializeComponent();
        }

        private void ReporteAlquiler_Load(object sender, EventArgs e)
        {
            this.alquiler_listar_detalle_reporteTableAdapter.Fill(this.dsALquiler.alquiler_listar_detalle_reporte, Variables.IdAlquiler);
            this.reportViewer1.RefreshReport();
            
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
