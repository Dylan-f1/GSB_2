using System;
using System.Linq;
using System.Windows.Forms;
using GSB_2.DAO;
using GSB_2.Models;

namespace GSB_2.Forms
{
    public partial class FormDoctor : Form
    {
        private readonly PatientDAO patientDAO;
        private readonly MedicineDAO medicineDAO;
        private readonly PrescriptionDAO prescriptionDAO;
        private int currentUserId;
        private bool userRole; // false = Doctor

        public FormDoctor(int userId, bool role)
        {
            InitializeComponent();

            currentUserId = userId;
            userRole = role;
            patientDAO = new PatientDAO();
            medicineDAO = new MedicineDAO();
            prescriptionDAO = new PrescriptionDAO();

            LoadPatients();
            LoadMedicines();
            LoadPrescriptionsData();
        }

        // ==================== PATIENT ====================

        private void LoadPatients()
        {
            try
            {
                var patientList = patientDAO.GetAll();

                if (patientList == null || patientList.Count == 0)
                {
                    dataGridViewPatients.DataSource = null;
                    return;
                }

                var displayList = patientList.Select(p => new
                {
                    Id = p.Id_patient,
                    Nom = p.Name,
                    Prénom = p.Firstname,
                    Age = p.Age,
                    Genre = p.Gender
                }).ToList();

                dataGridViewPatients.DataSource = displayList;
                dataGridViewPatients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des patients: {ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonPatientAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(textBoxPatientFirstname.Text))
                {
                    MessageBox.Show("Le prénom est obligatoire.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxPatientName.Text))
                {
                    MessageBox.Show("Le nom est obligatoire.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxPatientAge.Text))
                {
                    MessageBox.Show("L'âge est obligatoire.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(textBoxPatientAge.Text, out int age) || age <= 0)
                {
                    MessageBox.Show("L'âge doit être un nombre valide.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (comboBoxPatientGender.SelectedItem == null)
                {
                    MessageBox.Show("Le genre est obligatoire.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Appeler createPatient avec les paramètres individuels
                bool success = patientDAO.createPatient(
                    currentUserId,
                    textBoxPatientName.Text.Trim(),
                    age,
                    textBoxPatientFirstname.Text.Trim(),
                    comboBoxPatientGender.SelectedItem.ToString(),
                    userRole
                );

                if (success)
                {
                    LoadPatients();
                    ClearPatientFields();
                    MessageBox.Show("Patient ajouté avec succès !", "Succès",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Erreur lors de l'ajout du patient.", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonPatientDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewPatients.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Veuillez sélectionner un patient à supprimer.",
                        "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int id = Convert.ToInt32(dataGridViewPatients.SelectedRows[0].Cells["Id"].Value);

                var result = MessageBox.Show(
                    "Êtes-vous sûr de vouloir supprimer ce patient ?",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    bool success = patientDAO.deletePatient(id, userRole);

                    if (success)
                    {
                        LoadPatients();
                        ClearPatientFields();
                        MessageBox.Show("Patient supprimé avec succès !", "Succès",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la suppression du patient.", "Erreur",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonPatientClear_Click(object sender, EventArgs e)
        {
            ClearPatientFields();
        }

        private void ClearPatientFields()
        {
            textBoxPatientFirstname.Clear();
            textBoxPatientName.Clear();
            textBoxPatientAge.Clear();
            if (comboBoxPatientGender.Items.Count > 0)
                comboBoxPatientGender.SelectedIndex = 0;
            dataGridViewPatients.ClearSelection();
        }

        private void dataGridViewPatients_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewPatients.SelectedRows.Count > 0)
            {
                var row = dataGridViewPatients.SelectedRows[0];
                textBoxPatientFirstname.Text = row.Cells["Prénom"].Value?.ToString() ?? "";
                textBoxPatientName.Text = row.Cells["Nom"].Value?.ToString() ?? "";
                textBoxPatientAge.Text = row.Cells["Age"].Value?.ToString() ?? "";

                string gender = row.Cells["Genre"].Value?.ToString() ?? "";
                if (!string.IsNullOrEmpty(gender))
                {
                    comboBoxPatientGender.SelectedItem = gender;
                }
            }
        }

        // ==================== MEDICINE ====================

        private void LoadMedicines()
        {
            try
            {
                var medicineList = medicineDAO.GetAll();

                if (medicineList == null || medicineList.Count == 0)
                {
                    dataGridViewMedicines.DataSource = null;
                    return;
                }

                var displayList = medicineList.Select(m => new
                {
                    Id = m.Id_medicine,
                    Nom = m.Name,
                    Dosage = m.Dosage,
                    Molécule = m.Molecule,
                    Description = m.Description
                }).ToList();

                dataGridViewMedicines.DataSource = displayList;
                dataGridViewMedicines.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des médicaments: {ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonMedicineAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBoxMedicineName.Text))
                {
                    MessageBox.Show("Le nom du médicament est obligatoire.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxMedicineDosage.Text))
                {
                    MessageBox.Show("Le dosage est obligatoire.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(textBoxMedicineDosage.Text, out int dosage) || dosage <= 0)
                {
                    MessageBox.Show("Le dosage doit être un nombre valide.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxMedicineMolecule.Text))
                {
                    MessageBox.Show("La molécule est obligatoire.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Appeler createMedicine avec les paramètres individuels
                bool success = medicineDAO.createMedicine(
                    currentUserId,                               
                    textBoxMedicineName.Text.Trim(),            
                    textBoxMedicineDescription.Text.Trim(),     
                    textBoxMedicineMolecule.Text.Trim(),       
                    dosage,                                     
                    userRole                                    
                );

                if (success)
                {
                    LoadMedicines();
                    ClearMedicineFields();
                    MessageBox.Show("Médicament ajouté avec succès !", "Succès",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Erreur lors de l'ajout du médicament.", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonMedicineDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewMedicines.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Veuillez sélectionner un médicament à supprimer.",
                        "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int id = Convert.ToInt32(dataGridViewMedicines.SelectedRows[0].Cells["Id"].Value);

                var result = MessageBox.Show(
                    "Êtes-vous sûr de vouloir supprimer ce médicament ?",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    bool success = medicineDAO.deleteMedicine(id, userRole);

                    if (success)
                    {
                        LoadMedicines();
                        ClearMedicineFields();
                        MessageBox.Show("Médicament supprimé avec succès !", "Succès",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la suppression du médicament.", "Erreur",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonMedicineClear_Click(object sender, EventArgs e)
        {
            ClearMedicineFields();
        }

        private void ClearMedicineFields()
        {
            textBoxMedicineName.Clear();
            textBoxMedicineDosage.Clear();
            textBoxMedicineMolecule.Clear();
            textBoxMedicineDescription.Clear();
            dataGridViewMedicines.ClearSelection();
        }

        private void dataGridViewMedicines_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewMedicines.SelectedRows.Count > 0)
            {
                var row = dataGridViewMedicines.SelectedRows[0];
                textBoxMedicineName.Text = row.Cells["Nom"].Value?.ToString() ?? "";
                textBoxMedicineDosage.Text = row.Cells["Dosage"].Value?.ToString() ?? "";
                textBoxMedicineMolecule.Text = row.Cells["Molécule"].Value?.ToString() ?? "";
                textBoxMedicineDescription.Text = row.Cells["Description"].Value?.ToString() ?? "";
            }
        }

        // ==================== PRESCRIPTION ====================

        private void LoadPrescriptionsData()
        {
            try
            {
                // Charger les ComboBox
                var patients = patientDAO.GetAll();
                comboBoxPatient.DataSource = patients;
                comboBoxPatient.DisplayMember = "Firstname";
                comboBoxPatient.ValueMember = "Id_patient";

                // Charger les médicaments pour la ComboBox
                var medicines = medicineDAO.GetAll();
                comboBoxMedicine.DataSource = medicines;
                comboBoxMedicine.DisplayMember = "Name";
                comboBoxMedicine.ValueMember = "Id_medicine";

                // Charger le DataGridView des prescriptions
                var prescriptionList = prescriptionDAO.getAllPrescription();

                if (prescriptionList == null || prescriptionList.Count == 0)
                {
                    dataGridViewPrescriptions.DataSource = null;
                    return;
                }

                var displayList = prescriptionList.Select(p => new
                {
                    Id = p.Id_prescription,
                    Id_Patient = p.Id_patient,
                    Validité = p.Validity.ToString("dd/MM/yyyy")
                }).ToList();

                dataGridViewPrescriptions.DataSource = displayList;
                dataGridViewPrescriptions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des prescriptions: {ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonPrescriptionAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxPatient.SelectedValue == null)
                {
                    MessageBox.Show("Veuillez sélectionner un patient.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (comboBoxMedicine.SelectedValue == null)
                {
                    MessageBox.Show("Veuillez sélectionner un médicament.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxQuantity.Text))
                {
                    MessageBox.Show("Veuillez entrer une quantité.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(textBoxQuantity.Text, out int quantity) || quantity <= 0)
                {
                    MessageBox.Show("La quantité doit être un nombre valide.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 1. Créer la prescription
                bool prescriptionCreated = prescriptionDAO.createPrescription(
                    currentUserId,
                    (int)comboBoxPatient.SelectedValue,
                    dateTimePickerValidity.Value,
                    userRole
                );

                if (prescriptionCreated)
                {
                    // 2. Récupérer l'ID de la dernière prescription créée
                    int lastPrescriptionId = prescriptionDAO.getLastInsertedId();

                    // 3. Ajouter le médicament à la prescription avec AppartientDAO
                    AppartientDAO appartientDAO = new AppartientDAO();
                    bool medicineAdded = appartientDAO.addMedicineToPrescrition(
                        lastPrescriptionId,
                        (int)comboBoxMedicine.SelectedValue,
                        quantity
                    );

                    if (medicineAdded)
                    {
                        LoadPrescriptionsData();
                        ClearPrescriptionFields();
                        MessageBox.Show("Prescription créée et médicament ajouté avec succès !", "Succès",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Prescription créée mais erreur lors de l'ajout du médicament.", "Avertissement",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Erreur lors de la création de la prescription.", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonPrescriptionDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewPrescriptions.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Veuillez sélectionner une prescription à supprimer.",
                        "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int id = Convert.ToInt32(dataGridViewPrescriptions.SelectedRows[0].Cells["Id"].Value);

                var result = MessageBox.Show(
                    "Êtes-vous sûr de vouloir supprimer cette prescription et tous ses médicaments ?",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    // 1. Supprimer tous les médicaments de la prescription
                    AppartientDAO appartientDAO = new AppartientDAO();
                    appartientDAO.removeAllMedicinesFromPrescription(id);

                    // 2. Supprimer la prescription
                    bool success = prescriptionDAO.deletePrescription(id, userRole);

                    if (success)
                    {
                        LoadPrescriptionsData();
                        ClearPrescriptionFields();
                        MessageBox.Show("Prescription supprimée avec succès !", "Succès",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la suppression de la prescription.", "Erreur",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonPrescriptionClear_Click(object sender, EventArgs e)
        {
            ClearPrescriptionFields();
        }

        private void ClearPrescriptionFields()
        {
            if (comboBoxPatient.Items.Count > 0)
                comboBoxPatient.SelectedIndex = 0;

            if (comboBoxMedicine.Items.Count > 0)
                comboBoxMedicine.SelectedIndex = 0;

            textBoxQuantity.Clear();
            dateTimePickerValidity.Value = DateTime.Now;
            dataGridViewPrescriptions.ClearSelection();
        }
        
        // ==================== LOGOUT ====================
        private void buttonFormDoctorLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Êtes-vous sûr de vouloir vous déconnecter ?",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                this.Hide();
                MainForm loginForm = new MainForm();
                loginForm.Show();
            }
        }
    }
}