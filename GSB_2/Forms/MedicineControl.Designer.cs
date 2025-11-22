namespace GSB_2.Forms
{
    partial class MedicineControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridViewListMedicine = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridViewListMedicine).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewListMedicine
            // 
            dataGridViewListMedicine.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewListMedicine.Location = new Point(204, 102);
            dataGridViewListMedicine.Name = "dataGridViewListMedicine";
            dataGridViewListMedicine.RowHeadersWidth = 51;
            dataGridViewListMedicine.Size = new Size(456, 297);
            dataGridViewListMedicine.TabIndex = 1;
            // 
            // MedicineControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dataGridViewListMedicine);
            Name = "MedicineControl";
            Size = new Size(693, 438);
            ((System.ComponentModel.ISupportInitialize)dataGridViewListMedicine).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridViewListMedicine;
    }
}
