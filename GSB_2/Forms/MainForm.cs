using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GSB2.DAO;
using System.Security.Cryptography;
using ZstdSharp.Unsafe;
using GSB_2.DAO;
using GSB_2.Models;

namespace GSB_2.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            groupBoxLogin = new GroupBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            textBoxLoginPassword = new TextBox();
            textBoxLoginEmail = new TextBox();
            buttonLogin = new Button();
            groupBoxLogin.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxLogin
            // 
            groupBoxLogin.Controls.Add(label3);
            groupBoxLogin.Controls.Add(label2);
            groupBoxLogin.Controls.Add(label1);
            groupBoxLogin.Controls.Add(textBoxLoginPassword);
            groupBoxLogin.Controls.Add(textBoxLoginEmail);
            groupBoxLogin.Controls.Add(buttonLogin);
            groupBoxLogin.Location = new Point(191, 41);
            groupBoxLogin.Name = "groupBoxLogin";
            groupBoxLogin.Size = new Size(340, 317);
            groupBoxLogin.TabIndex = 0;
            groupBoxLogin.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(138, 23);
            label3.Name = "label3";
            label3.Size = new Size(46, 20);
            label3.TabIndex = 10;
            label3.Text = "Login";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(66, 92);
            label2.Name = "label2";
            label2.Size = new Size(53, 20);
            label2.TabIndex = 4;
            label2.Text = "Email :";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(66, 177);
            label1.Name = "label1";
            label1.Size = new Size(77, 20);
            label1.TabIndex = 3;
            label1.Text = "Password :";
            // 
            // textBoxLoginPassword
            // 
            textBoxLoginPassword.Location = new Point(66, 200);
            textBoxLoginPassword.Name = "textBoxLoginPassword";
            textBoxLoginPassword.Size = new Size(201, 27);
            textBoxLoginPassword.TabIndex = 2;
            textBoxLoginPassword.UseSystemPasswordChar = true;
            // 
            // textBoxLoginEmail
            // 
            textBoxLoginEmail.Location = new Point(66, 115);
            textBoxLoginEmail.Name = "textBoxLoginEmail";
            textBoxLoginEmail.Size = new Size(201, 27);
            textBoxLoginEmail.TabIndex = 1;
            // 
            // buttonLogin
            // 
            buttonLogin.Location = new Point(113, 249);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(94, 29);
            buttonLogin.TabIndex = 0;
            buttonLogin.Text = "Login";
            buttonLogin.UseVisualStyleBackColor = true;
            buttonLogin.Click += buttonLogin_Click;
            // 
            // MainForm
            // 
            ClientSize = new Size(753, 476);
            Controls.Add(groupBoxLogin);
            Name = "MainForm";
            groupBoxLogin.ResumeLayout(false);
            groupBoxLogin.PerformLayout();
            ResumeLayout(false);
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string email = this.textBoxLoginEmail.Text.Trim();
                string password = this.textBoxLoginPassword.Text.Trim();

                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Veuillez remplir tous les champs.", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                UserDAO userDAO = new UserDAO();
                User user = userDAO.Login(email, password);

                if (user != null)
                {
                    this.Hide();

                    if (user.Role == true) 
                    {
                        FormAdmin formAdmin = new FormAdmin();
                        formAdmin.Show();
                    }
                    else 
                    {
                        FormDoctor formDoctor = new FormDoctor(user.Id); 
                        formDoctor.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Email ou mot de passe incorrect.", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de connexion : {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}