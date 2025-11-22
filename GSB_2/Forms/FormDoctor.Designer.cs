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
            dataGridViewDoctorListMedicine = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDoctorListMedicine).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewDoctorListMedicine
            // 
            dataGridViewDoctorListMedicine.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewDoctorListMedicine.Location = new Point(423, 12);
            dataGridViewDoctorListMedicine.Name = "dataGridViewDoctorListMedicine";
            dataGridViewDoctorListMedicine.RowHeadersWidth = 51;
            dataGridViewDoctorListMedicine.Size = new Size(365, 245);
            dataGridViewDoctorListMedicine.TabIndex = 0;
            // 
            // FormDoctor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridViewDoctorListMedicine);
            Name = "FormDoctor";
            Text = "FormDoctor";
            ((System.ComponentModel.ISupportInitialize)dataGridViewDoctorListMedicine).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridViewDoctorListMedicine;
    }
}