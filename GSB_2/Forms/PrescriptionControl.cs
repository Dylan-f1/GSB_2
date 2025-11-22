using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GSB_2.DAO;
using GSB_2.Models;

namespace GSB_2.Forms
{
    public partial class PrescriptionControl : UserControl
    {
        private DataGridView dataGridViewPrescription;
        private Button btnCreate;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnViewMedicine;
        private Button btnRefresh;
        private readonly PrescriptionDAO prescriptionDAO;
        private readonly PatientDAO patientDAO;
        private readonly AppartientDAO appartientDAO;
        private readonly MedicineDAO medicineDAO;
        private int currentUserId;

        public PrescriptionControl(int userId)
        {
            InitializeComponent();
            currentUserId = userId;
            prescriptionDAO = new PrescriptionDAO();
            patientDAO = new PatientDAO();
            appartientDAO = new AppartientDAO();
            medicineDAO = new MedicineDAO();

            InitializeCustomComponents();
            LoadPrescription();
        }

        private void InitializeCustomComponents()
        {
            // Configuration du UserControl
            this.BackColor = Color.White;
            this.Dock = DockStyle.Fill;

            // Titre
            Label lblTitle = new Label();
            lblTitle.Text = "📋 Gestion des Prescriptions";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.Location = new Point(20, 20);
            lblTitle.AutoSize = true;
            this.Controls.Add(lblTitle);

            // Panel des boutons
            Panel panelButtons = new Panel();
            panelButtons.Location = new Point(20, 60);
            panelButtons.Size = new Size(900, 45);

            btnCreate = CreateButton("Créer Prescription", 0, Color.FromArgb(0, 120, 215));
            btnCreate.Click += BtnCreate_Click;
            panelButtons.Controls.Add(btnCreate);

            btnEdit = CreateButton("Modifier", 160, Color.FromArgb(255, 140, 0));
            btnEdit.Click += BtnEdit_Click;
            panelButtons.Controls.Add(btnEdit);

            btnDelete = CreateButton("Supprimer", 280, Color.FromArgb(220, 50, 50));
            btnDelete.Click += BtnDelete_Click;
            panelButtons.Controls.Add(btnDelete);

            btnViewMedicine = CreateButton("Voir Médicaments", 400, Color.FromArgb(156, 39, 176));
            btnViewMedicine.Click += BtnViewMedicines_Click;
            panelButtons.Controls.Add(btnViewMedicine);

            btnRefresh = CreateButton("Actualiser", 570, Color.FromArgb(0, 150, 136));
            btnRefresh.Click += BtnRefresh_Click;
            panelButtons.Controls.Add(btnRefresh);

            this.Controls.Add(panelButtons);

            // DataGridView
            dataGridViewPrescription = new DataGridView();
            dataGridViewPrescription.Location = new Point(20, 115);
            dataGridViewPrescription.Size = new Size(950, 450);
            dataGridViewPrescription.ReadOnly = true;
            dataGridViewPrescription.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewPrescription.MultiSelect = false;
            dataGridViewPrescription.AllowUserToAddRows = false;
            dataGridViewPrescription.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewPrescription.RowHeadersVisible = false;

            // Style
            dataGridViewPrescription.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 120, 215);
            dataGridViewPrescription.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewPrescription.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridViewPrescription.ColumnHeadersHeight = 40;
            dataGridViewPrescription.EnableHeadersVisualStyles = false;
            dataGridViewPrescription.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255);

            this.Controls.Add(dataGridViewPrescription);
        }

        private Button CreateButton(string text, int xPosition, Color color)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Location = new Point(xPosition, 0);
            btn.Size = new Size(150, 40);
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;

            btn.MouseEnter += (s, e) => btn.BackColor = ControlPaint.Light(color, 0.2f);
            btn.MouseLeave += (s, e) => btn.BackColor = color;

            return btn;
        }

        private void LoadPrescription()
        {
            try
            {
                var prescriptions = prescriptionDAO.getAllPrescription();
                var patients = patientDAO.GetAll();

                var displayList = prescriptions.Select(p => {
                    var patient = patients.FirstOrDefault(pat => pat.Id == p.Id_patients);
                    int medicineCount = appartientDAO.getMedicineCountByPrescriptionId(p.Id);

                    return new
                    {
                        ID = p.Id,
                        Patient = patient != null ? $"{patient.Name} {patient.Firstname}" : "Inconnu",
                        Quantité = p.quantity,
                        Validité = p.validity.ToString("dd/MM/yyyy"),
                        NbMédicaments = medicineCount
                    };
                }).ToList();

                dataGridViewPrescription.DataSource = displayList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement : {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Créer une prescription
        private void BtnCreate_Click(object sender, EventArgs e)
        {
            Form formCreate = new Form();
            formCreate.Text = "Créer une Prescription";
            formCreate.Size = new Size(550, 400);
            formCreate.StartPosition = FormStartPosition.CenterParent;
            formCreate.FormBorderStyle = FormBorderStyle.FixedDialog;
            formCreate.MaximizeBox = false;

            // Sélection du patient
            Label lblPatient = new Label { Text = "Patient :", Location = new Point(20, 20), AutoSize = true };
            ComboBox cmbPatient = new ComboBox
            {
                Location = new Point(120, 17),
                Width = 350,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            var patients = patientDAO.GetAll();
            foreach (var patient in patients)
            {
                cmbPatient.Items.Add(new { Text = $"{patient.Name} {patient.Firstname} ({patient.Age} ans)", Value = patient.Id });
            }
            cmbPatient.DisplayMember = "Text";
            cmbPatient.ValueMember = "Value";

            // Quantité
            Label lblQuantity = new Label { Text = "Quantité :", Location = new Point(20, 60), AutoSize = true };
            NumericUpDown numQuantity = new NumericUpDown
            {
                Location = new Point(120, 57),
                Width = 100,
                Minimum = 1,
                Maximum = 1000,
                Value = 1
            };

            // Date de validité
            Label lblValidity = new Label { Text = "Validité :", Location = new Point(20, 100), AutoSize = true };
            DateTimePicker dtpValidity = new DateTimePicker
            {
                Location = new Point(120, 97),
                Width = 200,
                Format = DateTimePickerFormat.Short,
                MinDate = DateTime.Now
            };

            // Section médicaments
            Label lblMedicines = new Label
            {
                Text = "Médicaments à ajouter :",
                Location = new Point(20, 140),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true
            };

            ListBox lstMedicines = new ListBox
            {
                Location = new Point(20, 165),
                Size = new Size(350, 100),
                SelectionMode = SelectionMode.MultiExtended
            };

            var medicines = medicineDAO.GetAll();
            foreach (var medicine in medicines)
            {
                lstMedicines.Items.Add(new { Text = $"{medicine.Name} - {medicine.Dosage}", Value = medicine.Id });
            }
            lstMedicines.DisplayMember = "Text";
            lstMedicines.ValueMember = "Value";

            // Boutons
            Button btnSave = new Button
            {
                Text = "Enregistrer",
                Location = new Point(120, 290),
                Size = new Size(120, 40),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            Button btnCancel = new Button
            {
                Text = "Annuler",
                Location = new Point(250, 290),
                Size = new Size(120, 40),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            btnSave.Click += (s, ev) =>
            {
                if (cmbPatient.SelectedItem == null)
                {
                    MessageBox.Show("Veuillez sélectionner un patient.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (lstMedicines.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Veuillez sélectionner au moins un médicament.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Créer la prescription
                int patientId = ((dynamic)cmbPatient.SelectedItem).Value;
                bool success = prescriptionDAO.createPrescription(
                    currentUserId,
                    patientId,
                    (int)numQuantity.Value,
                    dtpValidity.Value,
                    false // userRole = false pour Doctor
                );

                if (success)
                {
                    // Récupérer l'ID de la prescription créée
                    var prescriptions = prescriptionDAO.getAllPrescription();
                    int prescriptionId = prescriptions.Max(p => p.Id);

                    // Ajouter les médicaments
                    foreach (var item in lstMedicines.SelectedItems)
                    {
                        int medicineId = ((dynamic)item).Value;
                        appartientDAO.addMedicineToPrescrition(prescriptionId, medicineId);
                    }

                    MessageBox.Show("Prescription créée avec succès !", "Succès",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    formCreate.Close();
                    LoadPrescription();
                }
                else
                {
                    MessageBox.Show("Erreur lors de la création.", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            btnCancel.Click += (s, ev) => formCreate.Close();

            formCreate.Controls.AddRange(new Control[]
            {
                lblPatient, cmbPatient,
                lblQuantity, numQuantity,
                lblValidity, dtpValidity,
                lblMedicines, lstMedicines,
                btnSave, btnCancel
            });

            formCreate.ShowDialog();
        }

        // Modifier une prescription
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewPrescription.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une prescription.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int prescriptionId = Convert.ToInt32(dataGridViewPrescription.SelectedRows[0].Cells["ID"].Value);
            var prescription = prescriptionDAO.getAllPrescription().FirstOrDefault(p => p.Id == prescriptionId);

            if (prescription == null) return;

            // Formulaire de modification (similaire à la création)
            Form formEdit = new Form();
            formEdit.Text = "Modifier une Prescription";
            formEdit.Size = new Size(550, 300);
            formEdit.StartPosition = FormStartPosition.CenterParent;
            formEdit.FormBorderStyle = FormBorderStyle.FixedDialog;

            Label lblQuantity = new Label { Text = "Quantité :", Location = new Point(20, 20), AutoSize = true };
            NumericUpDown numQuantity = new NumericUpDown
            {
                Location = new Point(120, 17),
                Width = 100,
                Minimum = 1,
                Maximum = 1000,
                Value = prescription.quantity
            };

            Label lblValidity = new Label { Text = "Validité :", Location = new Point(20, 60), AutoSize = true };
            DateTimePicker dtpValidity = new DateTimePicker
            {
                Location = new Point(120, 57),
                Width = 200,
                Format = DateTimePickerFormat.Short,
                Value = prescription.validity
            };

            Button btnSave = new Button
            {
                Text = "Enregistrer",
                Location = new Point(120, 120),
                Size = new Size(120, 40),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            Button btnCancel = new Button
            {
                Text = "Annuler",
                Location = new Point(250, 120),
                Size = new Size(120, 40),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            btnSave.Click += (s, ev) =>
            {
                bool success = prescriptionDAO.updatePrescription(
                    prescriptionId,
                    prescription.Id_users,
                    prescription.Id_patients,
                    (int)numQuantity.Value,
                    dtpValidity.Value,
                    false
                );

                if (success)
                {
                    MessageBox.Show("Prescription modifiée avec succès !", "Succès",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    formEdit.Close();
                    LoadPrescription();
                }
                else
                {
                    MessageBox.Show("Erreur lors de la modification.", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            btnCancel.Click += (s, ev) => formEdit.Close();

            formEdit.Controls.AddRange(new Control[]
            {
                lblQuantity, numQuantity,
                lblValidity, dtpValidity,
                btnSave, btnCancel
            });

            formEdit.ShowDialog();
        }

        // Supprimer une prescription
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewPrescription.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une prescription.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette prescription ?",
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int prescriptionId = Convert.ToInt32(dataGridViewPrescription.SelectedRows[0].Cells["ID"].Value);
                var prescription = prescriptionDAO.getAllPrescription().FirstOrDefault(p => p.Id == prescriptionId);

                if (prescription != null)
                {
                    // Supprimer d'abord les médicaments liés
                    appartientDAO.removeAllMedicinesFromPrescription(prescriptionId);

                    // Puis supprimer la prescription
                    bool success = prescriptionDAO.deletePrescription(
                        prescriptionId,
                        prescription.Id_users,
                        prescription.Id_patients,
                        prescription.quantity,
                        prescription.validity,
                        false
                    );

                    if (success)
                    {
                        MessageBox.Show("Prescription supprimée avec succès !", "Succès",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPrescription();
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la suppression.", "Erreur",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Voir les médicaments d'une prescription
        private void BtnViewMedicines_Click(object sender, EventArgs e)
        {
            if (dataGridViewPrescription.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une prescription.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int prescriptionId = Convert.ToInt32(dataGridViewPrescription.SelectedRows[0].Cells["ID"].Value);
            var medicines = appartientDAO.getMedicinesByPrescriptionId(prescriptionId);

            // Afficher les médicaments dans un formulaire
            Form formMedicines = new Form();
            formMedicines.Text = $"Médicaments de la prescription #{prescriptionId}";
            formMedicines.Size = new Size(700, 400);
            formMedicines.StartPosition = FormStartPosition.CenterParent;

            DataGridView dgvMedicines = new DataGridView();
            dgvMedicines.Dock = DockStyle.Fill;
            dgvMedicines.ReadOnly = true;
            dgvMedicines.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            var displayList = medicines.Select(m => new
            {
                Nom = m.Name,
                Molécule = m.Molecule,
                Dosage = m.Dosage,
                Description = m.Description
            }).ToList();

            dgvMedicines.DataSource = displayList;
            formMedicines.Controls.Add(dgvMedicines);

            formMedicines.ShowDialog();
        }

        // Actualiser
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadPrescription();
            MessageBox.Show("Liste actualisée !", "Information",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}