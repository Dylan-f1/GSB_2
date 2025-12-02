using System;
using System.Drawing;
using System.Windows.Forms;
using GSB_2.Forms;

namespace GSB_2.Forms
{
    public partial class FormDoctor : Form
    {
        private Panel sidePanel;
        private Panel contentPanel;
        private Button btnMedicines;
        private Button btnPrescriptions;
        private Button btnLogout;
        private Label lblUserInfo;
        private int currentUserId;
        private string currentUserName;

        public FormDoctor(int userId, string userName)
        {
            InitializeComponent();
            currentUserId = userId;
            currentUserName = userName;
            InitializeCustomComponents();

            LoadUserControl(new MedicineControl());
        }

        private void InitializeCustomComponents()
        {
            // Configuration du formulaire principal
            this.Text = "GSB - Espace Médecin";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 240);

            // ===== SIDE PANEL =====
            sidePanel = new Panel();
            sidePanel.Dock = DockStyle.Left;
            sidePanel.Width = 220;
            sidePanel.BackColor = Color.FromArgb(45, 45, 48);
            this.Controls.Add(sidePanel);

            // ===== CONTENT PANEL =====
            contentPanel = new Panel();
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.BackColor = Color.White;
            contentPanel.Padding = new Padding(10);
            contentPanel.Visible = true;

            this.Controls.Add(contentPanel);
            this.Controls.Add(sidePanel);

            // Logo/Titre de l'application
            Label lblAppTitle = new Label();
            lblAppTitle.Text = "GSB";   
            lblAppTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblAppTitle.ForeColor = Color.White;
            lblAppTitle.Location = new Point(15, 20);
            lblAppTitle.AutoSize = true;
            sidePanel.Controls.Add(lblAppTitle);

            // Informations utilisateur
            lblUserInfo = new Label();
            lblUserInfo.Text = $"👤 Dr. {currentUserName}";
            lblUserInfo.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblUserInfo.ForeColor = Color.FromArgb(200, 200, 200);
            lblUserInfo.Location = new Point(15, 60);
            lblUserInfo.AutoSize = true;
            sidePanel.Controls.Add(lblUserInfo);

            // Ligne de séparation
            Panel separator = new Panel();
            separator.BackColor = Color.FromArgb(70, 70, 70);
            separator.Location = new Point(10, 95);
            separator.Size = new Size(200, 2);
            sidePanel.Controls.Add(separator);

            // Bouton Médicaments
            btnMedicines = CreateSideButton("💊 Médicaments", 110);
            btnMedicines.Click += BtnMedicines_Click;
            sidePanel.Controls.Add(btnMedicines);

            // Bouton Prescriptions
            btnPrescriptions = CreateSideButton("📋 Prescriptions", 160);
            btnPrescriptions.Click += BtnPrescriptions_Click;
            sidePanel.Controls.Add(btnPrescriptions);

            // Ligne de séparation en bas
            Panel separatorBottom = new Panel();
            separatorBottom.BackColor = Color.FromArgb(70, 70, 70);
            separatorBottom.Location = new Point(10, 580);
            separatorBottom.Size = new Size(200, 2);
            sidePanel.Controls.Add(separatorBottom);

            // Bouton Déconnexion
            btnLogout = CreateSideButton("Déconnexion", 595);
            btnLogout.BackColor = Color.FromArgb(192, 57, 43);
            btnLogout.Click += BtnLogout_Click;
            sidePanel.Controls.Add(btnLogout);
        }

        private Button CreateSideButton(string text, int yPosition)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Location = new Point(10, yPosition);
            btn.Size = new Size(200, 45);
            btn.BackColor = Color.FromArgb(45, 45, 48);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Cursor = Cursors.Hand;
            btn.Padding = new Padding(15, 0, 0, 0);

            // Effet hover
            btn.MouseEnter += (s, e) => {
                if (btn.BackColor != Color.FromArgb(0, 120, 215))
                    btn.BackColor = Color.FromArgb(60, 60, 65);
            };
            btn.MouseLeave += (s, e) => {
                if (btn.BackColor != Color.FromArgb(0, 120, 215))
                    btn.BackColor = Color.FromArgb(45, 45, 48);
            };

            return btn;
        }


        private void LoadUserControl(UserControl userControl)
        {
            try
            {
                // Nettoyer le contentPanel
                contentPanel.Controls.Clear();

                // Ajouter le nouveau UserControl
                userControl.Dock = DockStyle.Fill;
                userControl.Visible = true;
                contentPanel.Controls.Add(userControl);

               
                userControl.BringToFront();
                contentPanel.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement: {ex.Message}\n{ex.StackTrace}");
            }
        }

        private void BtnMedicines_Click(object sender, EventArgs e)
        {
            LoadUserControl(new MedicineControl());
        }

        private void BtnPrescriptions_Click(object sender, EventArgs e)
        {
            LoadUserControl(new PrescriptionControl(currentUserId));
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Êtes-vous sûr de vouloir vous déconnecter ?",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                this.Hide();
                MainForm loginForm = new MainForm();
                loginForm.Show();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBox.Show(
                    "Voulez-vous vraiment quitter l'application ?",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}