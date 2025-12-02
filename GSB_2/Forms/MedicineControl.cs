using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GSB_2.DAO;
namespace GSB_2.Forms
{
    public partial class MedicineControl : UserControl
    {
        public MedicineControl()
        {
            InitializeComponent();
            InitializeCustomComponents();
            LoadMedicines();
        }
        private void InitializeCustomComponents()
        {
            // Configuration du UserControl
            this.BackColor = Color.White;
            this.Dock = DockStyle.Fill;
            // Titre
            Label lblTitle = new Label();
            lblTitle.Text = "Liste des Médicaments";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.Location = new Point(20, 20);
            lblTitle.AutoSize = true;
            this.Controls.Add(lblTitle);
            // DataGridView
            dataGridViewListMedicine = new DataGridView();
            dataGridViewListMedicine.Location = new Point(20, 70);
            dataGridViewListMedicine.Size = new Size(950, 500);
            dataGridViewListMedicine.ReadOnly = true;
            dataGridViewListMedicine.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewListMedicine.MultiSelect = false;
            dataGridViewListMedicine.AllowUserToAddRows = false;
            this.Controls.Add(dataGridViewListMedicine);
        }
        private void LoadMedicines()
        {
            MedicineDAO medDAO = new MedicineDAO();
            var medList = medDAO.GetAll();
            var displayList = medList.Select(m => new
            {
                Id = m.Id,
                Name = m.Name,
                Molecule = m.Molecule,
                Dosage = m.Dosage,
                Description = m.Description
            }).ToList();
            this.dataGridViewListMedicine.DataSource = displayList;
            this.dataGridViewListMedicine.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}