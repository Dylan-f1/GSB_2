namespace GSB_2.Forms
{
    partial class FormAdmin
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
            dataGridViewListUser = new DataGridView();
            buttonAdminAdd = new Button();
            buttonAdminDelete = new Button();
            textBoxAdminFirstname = new TextBox();
            textBoxAdminName = new TextBox();
            labelAdminFirstName = new Label();
            labelAdminName = new Label();
            labelAdminEmail = new Label();
            labelAdminRole = new Label();
            comboBoxAdminRole = new ComboBox();
            textBoxAdminEmail = new TextBox();
            textBoxAdminPassword = new TextBox();
            labelAdminPassword = new Label();
            buttonAdminClear = new Button();
            buttonFormAdminLogout = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewListUser).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewListUser
            // 
            dataGridViewListUser.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewListUser.Location = new Point(287, 49);
            dataGridViewListUser.Name = "dataGridViewListUser";
            dataGridViewListUser.RowHeadersWidth = 51;
            dataGridViewListUser.Size = new Size(501, 368);
            dataGridViewListUser.TabIndex = 0;
            // 
            // buttonAdminAdd
            // 
            buttonAdminAdd.Location = new Point(12, 388);
            buttonAdminAdd.Name = "buttonAdminAdd";
            buttonAdminAdd.Size = new Size(94, 29);
            buttonAdminAdd.TabIndex = 1;
            buttonAdminAdd.Text = "Add";
            buttonAdminAdd.UseVisualStyleBackColor = true;
            buttonAdminAdd.Click += buttonAdminAdd_Click;
            // 
            // buttonAdminDelete
            // 
            buttonAdminDelete.Location = new Point(157, 388);
            buttonAdminDelete.Name = "buttonAdminDelete";
            buttonAdminDelete.Size = new Size(94, 29);
            buttonAdminDelete.TabIndex = 2;
            buttonAdminDelete.Text = "Delete";
            buttonAdminDelete.UseVisualStyleBackColor = true;
            buttonAdminDelete.Click += buttonAdminDelete_Click;
            // 
            // textBoxAdminFirstname
            // 
            textBoxAdminFirstname.Location = new Point(32, 49);
            textBoxAdminFirstname.Name = "textBoxAdminFirstname";
            textBoxAdminFirstname.Size = new Size(164, 27);
            textBoxAdminFirstname.TabIndex = 3;
            // 
            // textBoxAdminName
            // 
            textBoxAdminName.Location = new Point(32, 113);
            textBoxAdminName.Name = "textBoxAdminName";
            textBoxAdminName.Size = new Size(164, 27);
            textBoxAdminName.TabIndex = 4;
            // 
            // labelAdminFirstName
            // 
            labelAdminFirstName.AutoSize = true;
            labelAdminFirstName.Location = new Point(32, 26);
            labelAdminFirstName.Name = "labelAdminFirstName";
            labelAdminFirstName.Size = new Size(80, 20);
            labelAdminFirstName.TabIndex = 5;
            labelAdminFirstName.Text = "Firstname :";
            // 
            // labelAdminName
            // 
            labelAdminName.AutoSize = true;
            labelAdminName.Location = new Point(32, 90);
            labelAdminName.Name = "labelAdminName";
            labelAdminName.Size = new Size(56, 20);
            labelAdminName.TabIndex = 6;
            labelAdminName.Text = "Name :";
            // 
            // labelAdminEmail
            // 
            labelAdminEmail.AutoSize = true;
            labelAdminEmail.Location = new Point(32, 158);
            labelAdminEmail.Name = "labelAdminEmail";
            labelAdminEmail.Size = new Size(53, 20);
            labelAdminEmail.TabIndex = 7;
            labelAdminEmail.Text = "Email :";
            // 
            // labelAdminRole
            // 
            labelAdminRole.AutoSize = true;
            labelAdminRole.Location = new Point(32, 294);
            labelAdminRole.Name = "labelAdminRole";
            labelAdminRole.Size = new Size(46, 20);
            labelAdminRole.TabIndex = 8;
            labelAdminRole.Text = "Rôle :";
            // 
            // comboBoxAdminRole
            // 
            comboBoxAdminRole.FormattingEnabled = true;
            comboBoxAdminRole.Items.AddRange(new object[] { "Admin", "Doctor" });
            comboBoxAdminRole.Location = new Point(32, 317);
            comboBoxAdminRole.Name = "comboBoxAdminRole";
            comboBoxAdminRole.Size = new Size(86, 28);
            comboBoxAdminRole.TabIndex = 9;
            // 
            // textBoxAdminEmail
            // 
            textBoxAdminEmail.Location = new Point(32, 181);
            textBoxAdminEmail.Name = "textBoxAdminEmail";
            textBoxAdminEmail.Size = new Size(164, 27);
            textBoxAdminEmail.TabIndex = 10;
            // 
            // textBoxAdminPassword
            // 
            textBoxAdminPassword.Location = new Point(32, 251);
            textBoxAdminPassword.Name = "textBoxAdminPassword";
            textBoxAdminPassword.Size = new Size(164, 27);
            textBoxAdminPassword.TabIndex = 11;
            // 
            // labelAdminPassword
            // 
            labelAdminPassword.AutoSize = true;
            labelAdminPassword.Location = new Point(32, 228);
            labelAdminPassword.Name = "labelAdminPassword";
            labelAdminPassword.Size = new Size(77, 20);
            labelAdminPassword.TabIndex = 12;
            labelAdminPassword.Text = "Password :";
            // 
            // buttonAdminClear
            // 
            buttonAdminClear.Location = new Point(214, 17);
            buttonAdminClear.Name = "buttonAdminClear";
            buttonAdminClear.Size = new Size(57, 29);
            buttonAdminClear.TabIndex = 13;
            buttonAdminClear.Text = "Clear";
            buttonAdminClear.UseVisualStyleBackColor = true;
            buttonAdminClear.Click += buttonAdminClear_Click;
            // 
            // buttonFormAdminLogout
            // 
            buttonFormAdminLogout.Location = new Point(694, 12);
            buttonFormAdminLogout.Name = "buttonFormAdminLogout";
            buttonFormAdminLogout.Size = new Size(94, 29);
            buttonFormAdminLogout.TabIndex = 14;
            buttonFormAdminLogout.Text = "Logout";
            buttonFormAdminLogout.UseVisualStyleBackColor = true;
            buttonFormAdminLogout.Click += buttonFormAdminLogout_Click;
            // 
            // FormAdmin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonFormAdminLogout);
            Controls.Add(buttonAdminClear);
            Controls.Add(labelAdminPassword);
            Controls.Add(textBoxAdminPassword);
            Controls.Add(textBoxAdminEmail);
            Controls.Add(comboBoxAdminRole);
            Controls.Add(labelAdminRole);
            Controls.Add(labelAdminEmail);
            Controls.Add(labelAdminName);
            Controls.Add(labelAdminFirstName);
            Controls.Add(textBoxAdminName);
            Controls.Add(textBoxAdminFirstname);
            Controls.Add(buttonAdminDelete);
            Controls.Add(buttonAdminAdd);
            Controls.Add(dataGridViewListUser);
            Name = "FormAdmin";
            Text = "FormAdmin";
            ((System.ComponentModel.ISupportInitialize)dataGridViewListUser).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewListUser;
        private Button buttonAdminAdd;
        private Button buttonAdminDelete;
        private TextBox textBoxAdminFirstname;
        private TextBox textBoxAdminName;
        private Label labelAdminFirstName;
        private Label labelAdminName;
        private Label labelAdminEmail;
        private Label labelAdminRole;
        private ComboBox comboBoxAdminRole;
        private TextBox textBoxAdminEmail;
        private TextBox textBoxAdminPassword;
        private Label labelAdminPassword;
        private Button buttonAdminClear;
        private Button buttonFormAdminLogout;
    }
}