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
            textBoxLoginPassword = new TextBox();
            textBoxLoginEmail = new TextBox();
            buttonLogin = new Button();
            groupBoxLogin.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxLogin
            // 
            groupBoxLogin.Controls.Add(textBoxLoginPassword);
            groupBoxLogin.Controls.Add(textBoxLoginEmail);
            groupBoxLogin.Controls.Add(buttonLogin);
            groupBoxLogin.Location = new Point(191, 128);
            groupBoxLogin.Name = "groupBoxLogin";
            groupBoxLogin.Size = new Size(340, 230);
            groupBoxLogin.TabIndex = 0;
            groupBoxLogin.TabStop = false;
            // 
            // textBoxLoginPassword
            // 
            textBoxLoginPassword.Location = new Point(66, 119);
            textBoxLoginPassword.Name = "textBoxLoginPassword";
            textBoxLoginPassword.Size = new Size(201, 27);
            textBoxLoginPassword.TabIndex = 2;
            textBoxLoginPassword.UseSystemPasswordChar = true;
            // 
            // textBoxLoginEmail
            // 
            textBoxLoginEmail.Location = new Point(66, 56);
            textBoxLoginEmail.Name = "textBoxLoginEmail";
            textBoxLoginEmail.Size = new Size(201, 27);
            textBoxLoginEmail.TabIndex = 1;
            // 
            // buttonLogin
            // 
            buttonLogin.Location = new Point(117, 180);
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
            SHA256 sha256 = SHA256.Create();
            byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(this.textBoxLoginPassword.Text));
            string hashString = BitConverter.ToString(hashValue).Replace("-", "").ToLowerInvariant();
            string email = this.textBoxLoginEmail.Text;
            UserDAO userDAO = new UserDAO();
            User user = userDAO.Login(email, hashString);
            if (user != null && user.Role == true)
            {
                this.Hide();
                FormAdmin formAdmin = new FormAdmin();
                formAdmin.Show();
            }
            else if (user != null && user.Role == false)
            {
                this.Hide();
                FormDoctor formDoctor = new FormDoctor();
                formDoctor.Show();
            }
            else
            {
                MessageBox.Show("Please retry an email and password.", "Error", MessageBoxButtons.OK);
            }
        }


    }
}
