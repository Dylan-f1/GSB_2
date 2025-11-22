using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using GSB_2.DAO;
using GSB_2.Models;
using Microsoft.VisualBasic.ApplicationServices;

namespace GSB_2.Forms
{
    public partial class FormDoctor : Form
    {
        public FormDoctor()
        {
            InitializeComponent();
            SetupLayout();
        }

        private void SetupLayout()
        {
            // Configuration de la fenêtre
            this.Text = "GSB - Espace Docteur";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            // ===== PANEL LATÉRAL (MENU) =====
            SidePanel = new Panel();
            SidePanel.Dock = DockStyle.Left;
            SidePanel.Width = 220;
            SidePanel.BackColor = Color.FromArgb(45, 45, 48);

            // Titre du menu
            Label menuTitle = new Label();
            menuTitle.Text = "MENU DOCTEUR";
            menuTitle.ForeColor = Color.White;
            menuTitle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            menuTitle.Location = new Point(15, 20);
            menuTitle.AutoSize = true;
            SidePanel.Controls.Add(menuTitle);

            // Ligne de séparation
            Panel separator = new Panel();
            separator.BackColor = Color.FromArgb(80, 80, 85);
            separator.Location = new Point(10, 50);
            separator.Size = new Size(200, 2);
            SidePanel.Controls.Add(separator);

            // Boutons du menu
            Button btnPrescriptions = CreateMenuButton("📋 Prescriptions", 70);
            btnPrescriptions.Click += (s, e) => LoadContent(new PrescriptionControl());
            SidePanel.Controls.Add(btnPrescriptions);

            Button btnMedicines = CreateMenuButton("💊 Médicaments", 120);
            btnMedicines.Click += (s, e) => LoadContent(new MedicineControl());
            SidePanel.Controls.Add(btnMedicines);

            // Bouton déconnexion en bas
            Button btnLogout = new Button();
            btnLogout.Text = "Déconnexion";
            btnLogout.Location = new Point(10, 600);
            btnLogout.Size = new Size(200, 40);
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.BackColor = Color.FromArgb(180, 40, 40);
            btnLogout.ForeColor = Color.White;
            btnLogout.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnLogout.TextAlign = ContentAlignment.MiddleLeft;
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.Click += (s, e) => {
                var result = MessageBox.Show("Voulez-vous vraiment vous déconnecter ?",
                    "Déconnexion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    this.Close();
                }
            };

            btnLogout.MouseEnter += (s, e) => btnLogout.BackColor = Color.FromArgb(200, 60, 60);
            btnLogout.MouseLeave += (s, e) => btnLogout.BackColor = Color.FromArgb(180, 40, 40);

            SidePanel.Controls.Add(btnLogout);

            this.Controls.Add(SidePanel);

            // ===== PANEL DE CONTENU =====
            ContentPanel = new Panel();
            ContentPanel.Dock = DockStyle.Fill;
            ContentPanel.BackColor = Color.White;
            this.Controls.Add(ContentPanel);

            // Charger le contenu par défaut (Prescriptions)
            LoadContent(new PrescriptionControl());
        }

        private Button CreateMenuButton(string text, int yPosition)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Location = new Point(10, yPosition);
            btn.Size = new Size(200, 45);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.FromArgb(60, 60, 65);
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(15, 0, 0, 0);
            btn.Cursor = Cursors.Hand;

            // Effet hover
            btn.MouseEnter += (s, e) => {
                btn.BackColor = Color.FromArgb(0, 120, 215);
            };
            btn.MouseLeave += (s, e) => {
                btn.BackColor = Color.FromArgb(60, 60, 65);
            };

            return btn;
        }

        private void LoadContent(UserControl control)
        {
            // Vider le panel de contenu
            ContentPanel.Controls.Clear();

            // Ajouter le nouveau UserControl
            control.Dock = DockStyle.Fill;
            ContentPanel.Controls.Add(control);
        }
    }
}