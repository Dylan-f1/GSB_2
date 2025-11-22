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

namespace GSB_2.Forms
{
    public partial class FormDoctor : Form
    {
        public FormDoctor()
        {
            InitializeComponent();
            LoadMedicines();
        }

        private void LoadMedicines()
        {
            MedicineDAO medDAO = new MedicineDAO();
            List<Medicine> medList = medDAO.GetAll();

            var displayList = medList.Select(m => new
            {
                Id = m.Id,
                IdUser = m.IdUser,
                Dosage = m.Dosage,
                Name = m.Name,
                Description = m.Description,
                Molecule = m.Molecule
            }).ToList();

            this.dataGridViewDoctorListMedicine.DataSource = displayList;
            this.dataGridViewDoctorListMedicine.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}