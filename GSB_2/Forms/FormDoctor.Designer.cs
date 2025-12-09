namespace GSB_2.Forms
{
    partial class FormDoctor
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
            tabControlFormDoctor = new TabControl();
            PatientPage = new TabPage();
            buttonPatientClear = new Button();
            buttonPatientDelete = new Button();
            buttonPatientAdd = new Button();
            labelPatientGender = new Label();
            comboBoxPatientGender = new ComboBox();
            labelPatientAge = new Label();
            labelPatientName = new Label();
            labelPatientFirstname = new Label();
            textBoxPatientAge = new TextBox();
            textBoxPatientName = new TextBox();
            textBoxPatientFirstname = new TextBox();
            dataGridViewPatients = new DataGridView();
            MedicinePage = new TabPage();
            labelMedicineMolecule = new Label();
            labelMedicineDescription = new Label();
            labelMedicineDosage = new Label();
            labelMedicineName = new Label();
            dataGridViewMedicines = new DataGridView();
            buttonMedicineClear = new Button();
            buttonMedicineDelete = new Button();
            buttonMedicineAdd = new Button();
            textBoxMedicineDescription = new TextBox();
            textBoxMedicineMolecule = new TextBox();
            textBoxMedicineDosage = new TextBox();
            textBoxMedicineName = new TextBox();
            PrescriptionPage = new TabPage();
            labelPrescriptionQuantity = new Label();
            labelPrescriptionMedicine = new Label();
            labelPrescriptionDate = new Label();
            labelPrescriptionName = new Label();
            textBoxQuantity = new TextBox();
            comboBoxMedicine = new ComboBox();
            dataGridViewPrescriptions = new DataGridView();
            buttonPrescriptionClear = new Button();
            buttonPrescriptionDelete = new Button();
            buttonPrescriptionAdd = new Button();
            dateTimePickerValidity = new DateTimePicker();
            comboBoxPatient = new ComboBox();
            buttonFormDoctorLogout = new Button();
            buttonExportPrescriptionPdf = new Button();
            tabControlFormDoctor.SuspendLayout();
            PatientPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPatients).BeginInit();
            MedicinePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewMedicines).BeginInit();
            PrescriptionPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPrescriptions).BeginInit();
            SuspendLayout();
            // 
            // tabControlFormDoctor
            // 
            tabControlFormDoctor.Controls.Add(PatientPage);
            tabControlFormDoctor.Controls.Add(MedicinePage);
            tabControlFormDoctor.Controls.Add(PrescriptionPage);
            tabControlFormDoctor.Location = new Point(2, 2);
            tabControlFormDoctor.Name = "tabControlFormDoctor";
            tabControlFormDoctor.SelectedIndex = 0;
            tabControlFormDoctor.Size = new Size(794, 448);
            tabControlFormDoctor.TabIndex = 0;
            // 
            // PatientPage
            // 
            PatientPage.Controls.Add(buttonPatientClear);
            PatientPage.Controls.Add(buttonPatientDelete);
            PatientPage.Controls.Add(buttonPatientAdd);
            PatientPage.Controls.Add(labelPatientGender);
            PatientPage.Controls.Add(comboBoxPatientGender);
            PatientPage.Controls.Add(labelPatientAge);
            PatientPage.Controls.Add(labelPatientName);
            PatientPage.Controls.Add(labelPatientFirstname);
            PatientPage.Controls.Add(textBoxPatientAge);
            PatientPage.Controls.Add(textBoxPatientName);
            PatientPage.Controls.Add(textBoxPatientFirstname);
            PatientPage.Controls.Add(dataGridViewPatients);
            PatientPage.Location = new Point(4, 29);
            PatientPage.Name = "PatientPage";
            PatientPage.Padding = new Padding(3);
            PatientPage.Size = new Size(786, 415);
            PatientPage.TabIndex = 2;
            PatientPage.Text = "Patient";
            PatientPage.UseVisualStyleBackColor = true;
            // 
            // buttonPatientClear
            // 
            buttonPatientClear.Location = new Point(254, 25);
            buttonPatientClear.Name = "buttonPatientClear";
            buttonPatientClear.Size = new Size(70, 29);
            buttonPatientClear.TabIndex = 11;
            buttonPatientClear.Text = "Clear";
            buttonPatientClear.UseVisualStyleBackColor = true;
            // 
            // buttonPatientDelete
            // 
            buttonPatientDelete.Location = new Point(194, 327);
            buttonPatientDelete.Name = "buttonPatientDelete";
            buttonPatientDelete.Size = new Size(79, 29);
            buttonPatientDelete.TabIndex = 10;
            buttonPatientDelete.Text = "Delete";
            buttonPatientDelete.UseVisualStyleBackColor = true;
            buttonPatientDelete.Click += buttonPatientDelete_Click;
            // 
            // buttonPatientAdd
            // 
            buttonPatientAdd.Location = new Point(33, 327);
            buttonPatientAdd.Name = "buttonPatientAdd";
            buttonPatientAdd.Size = new Size(71, 29);
            buttonPatientAdd.TabIndex = 9;
            buttonPatientAdd.Text = "Add";
            buttonPatientAdd.UseVisualStyleBackColor = true;
            buttonPatientAdd.Click += buttonPatientAdd_Click;
            // 
            // labelPatientGender
            // 
            labelPatientGender.AutoSize = true;
            labelPatientGender.Location = new Point(164, 186);
            labelPatientGender.Name = "labelPatientGender";
            labelPatientGender.Size = new Size(64, 20);
            labelPatientGender.TabIndex = 8;
            labelPatientGender.Text = "Gender :";
            // 
            // comboBoxPatientGender
            // 
            comboBoxPatientGender.FormattingEnabled = true;
            comboBoxPatientGender.Items.AddRange(new object[] { "Homme", "Femme" });
            comboBoxPatientGender.Location = new Point(164, 214);
            comboBoxPatientGender.Name = "comboBoxPatientGender";
            comboBoxPatientGender.Size = new Size(88, 28);
            comboBoxPatientGender.TabIndex = 7;
            // 
            // labelPatientAge
            // 
            labelPatientAge.AutoSize = true;
            labelPatientAge.Location = new Point(33, 186);
            labelPatientAge.Name = "labelPatientAge";
            labelPatientAge.Size = new Size(43, 20);
            labelPatientAge.TabIndex = 6;
            labelPatientAge.Text = "Age :";
            // 
            // labelPatientName
            // 
            labelPatientName.AutoSize = true;
            labelPatientName.Location = new Point(33, 112);
            labelPatientName.Name = "labelPatientName";
            labelPatientName.Size = new Size(56, 20);
            labelPatientName.TabIndex = 5;
            labelPatientName.Text = "Name :";
            // 
            // labelPatientFirstname
            // 
            labelPatientFirstname.AutoSize = true;
            labelPatientFirstname.Location = new Point(33, 45);
            labelPatientFirstname.Name = "labelPatientFirstname";
            labelPatientFirstname.Size = new Size(80, 20);
            labelPatientFirstname.TabIndex = 4;
            labelPatientFirstname.Text = "Firstname :";
            // 
            // textBoxPatientAge
            // 
            textBoxPatientAge.Location = new Point(33, 214);
            textBoxPatientAge.Name = "textBoxPatientAge";
            textBoxPatientAge.Size = new Size(80, 27);
            textBoxPatientAge.TabIndex = 3;
            // 
            // textBoxPatientName
            // 
            textBoxPatientName.Location = new Point(33, 135);
            textBoxPatientName.Name = "textBoxPatientName";
            textBoxPatientName.Size = new Size(145, 27);
            textBoxPatientName.TabIndex = 2;
            // 
            // textBoxPatientFirstname
            // 
            textBoxPatientFirstname.Location = new Point(33, 68);
            textBoxPatientFirstname.Name = "textBoxPatientFirstname";
            textBoxPatientFirstname.Size = new Size(145, 27);
            textBoxPatientFirstname.TabIndex = 1;
            // 
            // dataGridViewPatients
            // 
            dataGridViewPatients.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewPatients.Location = new Point(330, 17);
            dataGridViewPatients.Name = "dataGridViewPatients";
            dataGridViewPatients.RowHeadersWidth = 51;
            dataGridViewPatients.Size = new Size(431, 392);
            dataGridViewPatients.TabIndex = 0;
            // 
            // MedicinePage
            // 
            MedicinePage.Controls.Add(labelMedicineMolecule);
            MedicinePage.Controls.Add(labelMedicineDescription);
            MedicinePage.Controls.Add(labelMedicineDosage);
            MedicinePage.Controls.Add(labelMedicineName);
            MedicinePage.Controls.Add(dataGridViewMedicines);
            MedicinePage.Controls.Add(buttonMedicineClear);
            MedicinePage.Controls.Add(buttonMedicineDelete);
            MedicinePage.Controls.Add(buttonMedicineAdd);
            MedicinePage.Controls.Add(textBoxMedicineDescription);
            MedicinePage.Controls.Add(textBoxMedicineMolecule);
            MedicinePage.Controls.Add(textBoxMedicineDosage);
            MedicinePage.Controls.Add(textBoxMedicineName);
            MedicinePage.Location = new Point(4, 29);
            MedicinePage.Name = "MedicinePage";
            MedicinePage.Padding = new Padding(3);
            MedicinePage.Size = new Size(786, 415);
            MedicinePage.TabIndex = 0;
            MedicinePage.Text = "Medicine";
            MedicinePage.UseVisualStyleBackColor = true;
            // 
            // labelMedicineMolecule
            // 
            labelMedicineMolecule.AutoSize = true;
            labelMedicineMolecule.Location = new Point(45, 172);
            labelMedicineMolecule.Name = "labelMedicineMolecule";
            labelMedicineMolecule.Size = new Size(77, 20);
            labelMedicineMolecule.TabIndex = 11;
            labelMedicineMolecule.Text = "Molecule :";
            // 
            // labelMedicineDescription
            // 
            labelMedicineDescription.AutoSize = true;
            labelMedicineDescription.Location = new Point(45, 247);
            labelMedicineDescription.Name = "labelMedicineDescription";
            labelMedicineDescription.Size = new Size(92, 20);
            labelMedicineDescription.TabIndex = 10;
            labelMedicineDescription.Text = "Description :";
            // 
            // labelMedicineDosage
            // 
            labelMedicineDosage.AutoSize = true;
            labelMedicineDosage.Location = new Point(45, 101);
            labelMedicineDosage.Name = "labelMedicineDosage";
            labelMedicineDosage.Size = new Size(67, 20);
            labelMedicineDosage.TabIndex = 9;
            labelMedicineDosage.Text = "Dosage :";
            // 
            // labelMedicineName
            // 
            labelMedicineName.AutoSize = true;
            labelMedicineName.Location = new Point(45, 32);
            labelMedicineName.Name = "labelMedicineName";
            labelMedicineName.Size = new Size(56, 20);
            labelMedicineName.TabIndex = 8;
            labelMedicineName.Text = "Name :";
            // 
            // dataGridViewMedicines
            // 
            dataGridViewMedicines.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewMedicines.Location = new Point(351, 22);
            dataGridViewMedicines.Name = "dataGridViewMedicines";
            dataGridViewMedicines.RowHeadersWidth = 51;
            dataGridViewMedicines.Size = new Size(415, 370);
            dataGridViewMedicines.TabIndex = 7;
            // 
            // buttonMedicineClear
            // 
            buttonMedicineClear.Location = new Point(236, 35);
            buttonMedicineClear.Name = "buttonMedicineClear";
            buttonMedicineClear.Size = new Size(70, 29);
            buttonMedicineClear.TabIndex = 6;
            buttonMedicineClear.Text = "Clear";
            buttonMedicineClear.UseVisualStyleBackColor = true;
            buttonMedicineClear.Click += buttonMedicineClear_Click;
            // 
            // buttonMedicineDelete
            // 
            buttonMedicineDelete.Location = new Point(170, 339);
            buttonMedicineDelete.Name = "buttonMedicineDelete";
            buttonMedicineDelete.Size = new Size(70, 29);
            buttonMedicineDelete.TabIndex = 5;
            buttonMedicineDelete.Text = "Delete";
            buttonMedicineDelete.UseVisualStyleBackColor = true;
            buttonMedicineDelete.Click += buttonMedicineDelete_Click;
            // 
            // buttonMedicineAdd
            // 
            buttonMedicineAdd.Location = new Point(45, 339);
            buttonMedicineAdd.Name = "buttonMedicineAdd";
            buttonMedicineAdd.Size = new Size(72, 29);
            buttonMedicineAdd.TabIndex = 4;
            buttonMedicineAdd.Text = "Add";
            buttonMedicineAdd.UseVisualStyleBackColor = true;
            buttonMedicineAdd.Click += buttonMedicineAdd_Click;
            // 
            // textBoxMedicineDescription
            // 
            textBoxMedicineDescription.Location = new Point(45, 270);
            textBoxMedicineDescription.Name = "textBoxMedicineDescription";
            textBoxMedicineDescription.Size = new Size(125, 27);
            textBoxMedicineDescription.TabIndex = 3;
            // 
            // textBoxMedicineMolecule
            // 
            textBoxMedicineMolecule.Location = new Point(45, 195);
            textBoxMedicineMolecule.Name = "textBoxMedicineMolecule";
            textBoxMedicineMolecule.Size = new Size(125, 27);
            textBoxMedicineMolecule.TabIndex = 2;
            // 
            // textBoxMedicineDosage
            // 
            textBoxMedicineDosage.Location = new Point(45, 124);
            textBoxMedicineDosage.Name = "textBoxMedicineDosage";
            textBoxMedicineDosage.Size = new Size(125, 27);
            textBoxMedicineDosage.TabIndex = 1;
            // 
            // textBoxMedicineName
            // 
            textBoxMedicineName.Location = new Point(45, 55);
            textBoxMedicineName.Name = "textBoxMedicineName";
            textBoxMedicineName.Size = new Size(125, 27);
            textBoxMedicineName.TabIndex = 0;
            // 
            // PrescriptionPage
            // 
            PrescriptionPage.Controls.Add(buttonExportPrescriptionPdf);
            PrescriptionPage.Controls.Add(labelPrescriptionQuantity);
            PrescriptionPage.Controls.Add(labelPrescriptionMedicine);
            PrescriptionPage.Controls.Add(labelPrescriptionDate);
            PrescriptionPage.Controls.Add(labelPrescriptionName);
            PrescriptionPage.Controls.Add(textBoxQuantity);
            PrescriptionPage.Controls.Add(comboBoxMedicine);
            PrescriptionPage.Controls.Add(dataGridViewPrescriptions);
            PrescriptionPage.Controls.Add(buttonPrescriptionClear);
            PrescriptionPage.Controls.Add(buttonPrescriptionDelete);
            PrescriptionPage.Controls.Add(buttonPrescriptionAdd);
            PrescriptionPage.Controls.Add(dateTimePickerValidity);
            PrescriptionPage.Controls.Add(comboBoxPatient);
            PrescriptionPage.Location = new Point(4, 29);
            PrescriptionPage.Name = "PrescriptionPage";
            PrescriptionPage.Padding = new Padding(3);
            PrescriptionPage.Size = new Size(786, 415);
            PrescriptionPage.TabIndex = 1;
            PrescriptionPage.Text = " Prescription";
            PrescriptionPage.UseVisualStyleBackColor = true;
            // 
            // labelPrescriptionQuantity
            // 
            labelPrescriptionQuantity.AutoSize = true;
            labelPrescriptionQuantity.Location = new Point(21, 82);
            labelPrescriptionQuantity.Name = "labelPrescriptionQuantity";
            labelPrescriptionQuantity.Size = new Size(65, 20);
            labelPrescriptionQuantity.TabIndex = 11;
            labelPrescriptionQuantity.Text = "Quantity";
            // 
            // labelPrescriptionMedicine
            // 
            labelPrescriptionMedicine.AutoSize = true;
            labelPrescriptionMedicine.Location = new Point(21, 146);
            labelPrescriptionMedicine.Name = "labelPrescriptionMedicine";
            labelPrescriptionMedicine.Size = new Size(70, 20);
            labelPrescriptionMedicine.TabIndex = 10;
            labelPrescriptionMedicine.Text = "Medicine";
            // 
            // labelPrescriptionDate
            // 
            labelPrescriptionDate.AutoSize = true;
            labelPrescriptionDate.Location = new Point(21, 214);
            labelPrescriptionDate.Name = "labelPrescriptionDate";
            labelPrescriptionDate.Size = new Size(48, 20);
            labelPrescriptionDate.TabIndex = 9;
            labelPrescriptionDate.Text = "Date :";
            // 
            // labelPrescriptionName
            // 
            labelPrescriptionName.AutoSize = true;
            labelPrescriptionName.Location = new Point(21, 16);
            labelPrescriptionName.Name = "labelPrescriptionName";
            labelPrescriptionName.Size = new Size(56, 20);
            labelPrescriptionName.TabIndex = 8;
            labelPrescriptionName.Text = "Name :";
            // 
            // textBoxQuantity
            // 
            textBoxQuantity.Location = new Point(21, 105);
            textBoxQuantity.Name = "textBoxQuantity";
            textBoxQuantity.Size = new Size(125, 27);
            textBoxQuantity.TabIndex = 7;
            // 
            // comboBoxMedicine
            // 
            comboBoxMedicine.FormattingEnabled = true;
            comboBoxMedicine.Location = new Point(21, 169);
            comboBoxMedicine.Name = "comboBoxMedicine";
            comboBoxMedicine.Size = new Size(151, 28);
            comboBoxMedicine.TabIndex = 6;
            // 
            // dataGridViewPrescriptions
            // 
            dataGridViewPrescriptions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewPrescriptions.Location = new Point(426, 26);
            dataGridViewPrescriptions.Name = "dataGridViewPrescriptions";
            dataGridViewPrescriptions.RowHeadersWidth = 51;
            dataGridViewPrescriptions.Size = new Size(345, 364);
            dataGridViewPrescriptions.TabIndex = 5;
            // 
            // buttonPrescriptionClear
            // 
            buttonPrescriptionClear.Location = new Point(298, 39);
            buttonPrescriptionClear.Name = "buttonPrescriptionClear";
            buttonPrescriptionClear.Size = new Size(72, 29);
            buttonPrescriptionClear.TabIndex = 4;
            buttonPrescriptionClear.Text = "Clear";
            buttonPrescriptionClear.UseVisualStyleBackColor = true;
            buttonPrescriptionClear.Click += buttonPrescriptionClear_Click;
            // 
            // buttonPrescriptionDelete
            // 
            buttonPrescriptionDelete.Location = new Point(133, 349);
            buttonPrescriptionDelete.Name = "buttonPrescriptionDelete";
            buttonPrescriptionDelete.Size = new Size(94, 29);
            buttonPrescriptionDelete.TabIndex = 3;
            buttonPrescriptionDelete.Text = "Delete";
            buttonPrescriptionDelete.UseVisualStyleBackColor = true;
            buttonPrescriptionDelete.Click += buttonPrescriptionDelete_Click;
            // 
            // buttonPrescriptionAdd
            // 
            buttonPrescriptionAdd.Location = new Point(21, 349);
            buttonPrescriptionAdd.Name = "buttonPrescriptionAdd";
            buttonPrescriptionAdd.Size = new Size(69, 29);
            buttonPrescriptionAdd.TabIndex = 2;
            buttonPrescriptionAdd.Text = "Add";
            buttonPrescriptionAdd.UseVisualStyleBackColor = true;
            buttonPrescriptionAdd.Click += buttonPrescriptionAdd_Click;
            // 
            // dateTimePickerValidity
            // 
            dateTimePickerValidity.Location = new Point(21, 237);
            dateTimePickerValidity.Name = "dateTimePickerValidity";
            dateTimePickerValidity.Size = new Size(228, 27);
            dateTimePickerValidity.TabIndex = 1;
            // 
            // comboBoxPatient
            // 
            comboBoxPatient.FormattingEnabled = true;
            comboBoxPatient.Location = new Point(21, 39);
            comboBoxPatient.Name = "comboBoxPatient";
            comboBoxPatient.Size = new Size(151, 28);
            comboBoxPatient.TabIndex = 0;
            // 
            // buttonFormDoctorLogout
            // 
            buttonFormDoctorLogout.Location = new Point(702, 2);
            buttonFormDoctorLogout.Name = "buttonFormDoctorLogout";
            buttonFormDoctorLogout.Size = new Size(94, 29);
            buttonFormDoctorLogout.TabIndex = 1;
            buttonFormDoctorLogout.Text = "Logout";
            buttonFormDoctorLogout.UseVisualStyleBackColor = true;
            buttonFormDoctorLogout.Click += buttonFormDoctorLogout_Click;
            // 
            // buttonExportPrescriptionPdf
            // 
            buttonExportPrescriptionPdf.Location = new Point(21, 290);
            buttonExportPrescriptionPdf.Name = "buttonExportPrescriptionPdf";
            buttonExportPrescriptionPdf.Size = new Size(94, 29);
            buttonExportPrescriptionPdf.TabIndex = 12;
            buttonExportPrescriptionPdf.Text = "Export PDF";
            buttonExportPrescriptionPdf.UseVisualStyleBackColor = true;
            buttonExportPrescriptionPdf.Click += buttonExportPrescriptionPdf_Click;
            // 
            // FormDoctor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonFormDoctorLogout);
            Controls.Add(tabControlFormDoctor);
            Name = "FormDoctor";
            Text = "FormDoctor";
            tabControlFormDoctor.ResumeLayout(false);
            PatientPage.ResumeLayout(false);
            PatientPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPatients).EndInit();
            MedicinePage.ResumeLayout(false);
            MedicinePage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewMedicines).EndInit();
            PrescriptionPage.ResumeLayout(false);
            PrescriptionPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPrescriptions).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel SidePanel;
        private Button buttonPatientAdd;
        private Button BtnPrescriptions;
        private TabControl tabControlFormDoctor;
        private TabPage MedicinePage;
        private TabPage PrescriptionPage;
        private TabPage PatientPage;
        private DataGridView dataGridViewPatients;
        private TextBox textBoxPatientFirstname;
        private TextBox textBoxPatientName;
        private TextBox textBoxPatientAge;
        private Label labelPatientFirstname;
        private Label labelPatientName;
        private Label labelPatientAge;
        private ComboBox comboBoxPatientGender;
        private Label labelPatientGender;
        private Button buttonPatientDelete;
        private Button buttonPatientClear;
        private Button buttonFormDoctorLogout;
        private TextBox textBoxMedicineName;
        private TextBox textBoxMedicineDosage;
        private TextBox textBoxMedicineMolecule;
        private TextBox textBoxMedicineDescription;
        private Button buttonMedicineAdd;
        private Button buttonMedicineDelete;
        private Button buttonMedicineClear;
        private DataGridView dataGridViewMedicines;
        private Label labelMedicineName;
        private Label labelMedicineDosage;
        private Label labelMedicineDescription;
        private Label labelMedicineMolecule;
        private ComboBox comboBoxPatient;
        private DateTimePicker dateTimePickerValidity;
        private Button buttonPrescriptionAdd;
        private Button buttonPrescriptionDelete;
        private Button buttonPrescriptionClear;
        private DataGridView dataGridViewPrescriptions;
        private ComboBox comboBoxMedicine;
        private TextBox textBoxQuantity;
        private Label labelPrescriptionName;
        private Label labelPrescriptionDate;
        private Label labelPrescriptionMedicine;
        private Label labelPrescriptionQuantity;
        private Button buttonExportPrescriptionPdf;
    }
}