namespace Alquiler.Presentacion.Reportes
{
    partial class ReporteAlquiler
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dsALquiler = new Alquiler.Presentacion.Reportes.DsALquiler();
            this.dsALquilerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsALquilerBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.alquilerlistardetallereporteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.alquiler_listar_detalle_reporteTableAdapter = new Alquiler.Presentacion.Reportes.DsALquilerTableAdapters.alquiler_listar_detalle_reporteTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dsALquiler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsALquilerBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsALquilerBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alquilerlistardetallereporteBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.alquilerlistardetallereporteBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Alquiler.Presentacion.Reportes.ReporteAlquiler.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // dsALquiler
            // 
            this.dsALquiler.DataSetName = "DsALquiler";
            this.dsALquiler.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dsALquilerBindingSource
            // 
            this.dsALquilerBindingSource.DataSource = this.dsALquiler;
            this.dsALquilerBindingSource.Position = 0;
            // 
            // dsALquilerBindingSource1
            // 
            this.dsALquilerBindingSource1.DataSource = this.dsALquiler;
            this.dsALquilerBindingSource1.Position = 0;
            // 
            // alquilerlistardetallereporteBindingSource
            // 
            this.alquilerlistardetallereporteBindingSource.DataMember = "alquiler_listar_detalle_reporte";
            this.alquilerlistardetallereporteBindingSource.DataSource = this.dsALquiler;
            // 
            // alquiler_listar_detalle_reporteTableAdapter
            // 
            this.alquiler_listar_detalle_reporteTableAdapter.ClearBeforeFill = true;
            // 
            // ReporteAlquiler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReporteAlquiler";
            this.Text = "ReporteAlquiler";
            this.Load += new System.EventHandler(this.ReporteAlquiler_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dsALquiler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsALquilerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsALquilerBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alquilerlistardetallereporteBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource dsALquilerBindingSource1;
        private DsALquiler dsALquiler;
        private System.Windows.Forms.BindingSource dsALquilerBindingSource;
        private System.Windows.Forms.BindingSource alquilerlistardetallereporteBindingSource;
        private DsALquilerTableAdapters.alquiler_listar_detalle_reporteTableAdapter alquiler_listar_detalle_reporteTableAdapter;
    }
}