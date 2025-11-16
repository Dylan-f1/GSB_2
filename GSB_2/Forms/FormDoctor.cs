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
using GSB_2.Models;

namespace GSB_2.Forms
{
    public partial class FormDoctor : Form
    {
        public FormDoctor()
        {
            InitializeComponent();
            MedicineDAO medDAO = new MedicineDAO();
            List<Medicine> medList = medDAO.GetAll();

            this.dataGridViewDoctorListMedicine.DataSource = medList;

        }
    }
}
