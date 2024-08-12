﻿using System;
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
    public partial class FrmPrincipal : Form
    {
        private int childFormNumber = 0;

        public int IdUsuario;
        public int IdRol;
        public string Rol;
        public bool Estado;

        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Ventana " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCategoria frm = new FrmCategoria();
            frm.MdiParent = this;
            frm.Show();
        }

        private void marcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMarca frm = new FrmMarca();
            frm.MdiParent = this;
            frm.Show();
        }

        private void subcategoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSubcategoria frm = new FrmSubcategoria();
            frm.MdiParent = this;
            frm.Show();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUsuario frm = new FrmUsuario();
            frm.MdiParent = this;
            frm.Show();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            StBrraInferior.Text = "Desarrollado por Javier Torrico, Permiso: " + this.Rol;
            MessageBox.Show("Bienvenido", "Sistema de Alquiler", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (this.Rol.Equals("Administrador"))
            {
                MnuAccesos.Enabled = true;
                MnuAlmacen.Enabled = true;
                MnuAlquiler.Enabled = true;
                MnuConsultas.Enabled = true;
                MnuIngresos.Enabled = true;
            }
            else
            {
                if (this.Rol.Equals("Personal"))
                {
                    MnuAccesos.Enabled = false;
                    MnuAlmacen.Enabled = false;
                    MnuAlquiler.Enabled = true;
                    MnuConsultas.Enabled = true;
                    MnuIngresos.Enabled = false;
                }
                else
                {
                    MnuAccesos.Enabled = false;
                    MnuAlmacen.Enabled = false;
                    MnuAlquiler.Enabled = false;
                    MnuConsultas.Enabled = false;
                    MnuIngresos.Enabled = false;
                }
            }
        }

        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult Opcion = new DialogResult();
            Opcion = MessageBox.Show("Deseas salir del sistema?", "Sistema de Ventas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Opcion == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Application.ExitThread();
            }
            
            
            


        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult Opcion = new DialogResult();
            Opcion = MessageBox.Show("Deseas salir del sistema?", "Sistema de Ventas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Opcion == DialogResult.Yes)
            {
                Application.ExitThread();
            }
            



        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPersona frm = new FrmPersona();
            frm.MdiParent = this;
            frm.Show();
        }

        private void articulosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmArticulo frm = new FrmArticulo();
            frm.MdiParent = this;
            frm.Show();
        }

        private void comprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmIngreso frm = new FrmIngreso();
            frm.MdiParent = this;
            frm.Show();
        }

        private void ventasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmAlquiler frm = new FrmAlquiler();
            frm.MdiParent = this;
            frm.Show();
        }

        private void TsAlquiler_Click(object sender, EventArgs e)
        {
            FrmIngreso frm = new FrmIngreso();
            frm.MdiParent = this;
            frm.Show();
        }

        private void TsCompras_Click(object sender, EventArgs e)
        {
            FrmAlquiler frm = new FrmAlquiler();
            frm.MdiParent = this;
            frm.Show();
        }

        private void setsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmConjunto frm = new FrmConjunto();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
