using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using GSB_2.DAO;
using GSB_2.Models;
using GSB_2.Utils;

namespace GSB_2.Forms
{
    public partial class FormDoctor : Form
    {
        private readonly PatientDAO patientDAO;
        private readonly MedicineDAO medicineDAO;
        private readonly PrescriptionDAO prescriptionDAO;
        private readonly AppartientDAO appartientDAO;
        private readonly UserDAO userDAO;
        private readonly PrescriptionSearchDAO searchDAO;  // ← NOUVEAU
        private int currentUserId;
        private bool userRole; // false = Doctor

        // ← NOUVEAU : Stocker les résultats de recherche
        private List<PrescriptionSearchResult> currentSearchResults;

        public FormDoctor(int userId, bool role)
        {
            InitializeComponent();

            currentUserId = userId;
            userRole = role;
            patientDAO = new PatientDAO();
            medicineDAO = new MedicineDAO();
            prescriptionDAO = new PrescriptionDAO();
            appartientDAO = new AppartientDAO();
            userDAO = new UserDAO();
            searchDAO = new PrescriptionSearchDAO();  // ← NOUVEAU

            LoadPatients();
            LoadMedicines();
            LoadPrescriptionsData();
            InitializeSearchTab();  // ← NOUVEAU
        }

        // ==================== PATIENT ====================
        // [... Garder tout le code existant pour Patient ...]

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
        // [... Garder tout le code existant pour Medicine ...]

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
        // [... Garder tout le code existant pour Prescription ...]

        private void LoadPrescriptionsData()
        {
            try
            {
                var patients = patientDAO.GetAll();
                comboBoxPatient.DataSource = patients;
                comboBoxPatient.DisplayMember = "Firstname";
                comboBoxPatient.ValueMember = "Id_patient";

                var medicines = medicineDAO.GetAll();
                comboBoxMedicine.DataSource = medicines;
                comboBoxMedicine.DisplayMember = "Name";
                comboBoxMedicine.ValueMember = "Id_medicine";

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
                    Validité = p.Validity.ToString("dd/MM/yyyy"),
                    Nb_Médicaments = appartientDAO.getMedicineCountByPrescriptionId(p.Id_prescription)
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

                bool prescriptionCreated = prescriptionDAO.createPrescription(
                    currentUserId,
                    (int)comboBoxPatient.SelectedValue,
                    dateTimePickerValidity.Value,
                    userRole
                );

                if (prescriptionCreated)
                {
                    int lastPrescriptionId = prescriptionDAO.getLastInsertedId();

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
                    appartientDAO.removeAllMedicinesFromPrescription(id);
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

        private void buttonExportPrescriptionPdf_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewPrescriptions.SelectedRows.Count == 0)
                {
                    MessageBox.Show(
                        "Veuillez sélectionner une prescription à exporter.",
                        "Attention",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                int prescriptionId = Convert.ToInt32(
                    dataGridViewPrescriptions.SelectedRows[0].Cells["Id"].Value);

                Prescription prescription = prescriptionDAO.getPrescriptionById(prescriptionId);
                if (prescription == null)
                {
                    MessageBox.Show("Prescription introuvable.", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Patient patient = patientDAO.GetPatientById(prescription.Id_patient);
                User doctor = userDAO.GetUserById(prescription.Id_user);
                List<Medicine> medicines = appartientDAO.getMedicinesByPrescriptionId(prescriptionId);

                if (medicines == null || medicines.Count == 0)
                {
                    MessageBox.Show(
                        "Cette prescription ne contient aucun médicament.\n" +
                        "Impossible d'exporter une prescription vide.",
                        "Prescription vide",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                List<(Medicine, int)> medicinesWithQuantity = new List<(Medicine, int)>();
                foreach (Medicine med in medicines)
                {
                    int quantity = appartientDAO.getMedicineQuantity(prescriptionId, med.Id_medicine);
                    medicinesWithQuantity.Add((med, quantity));
                }

                ExporterPDF.ExportPrescription(prescription, patient, doctor, medicinesWithQuantity);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erreur lors de l'export :\n{ex.Message}",
                    "Erreur",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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

        // ==================== SEARCH TAB (NOUVEAU) ====================

        /// <summary>
        /// Initialise l'onglet de recherche au chargement du formulaire
        /// </summary>
        private void InitializeSearchTab()
        {
            try
            {
                // Charger la liste des médicaments dans la ComboBox de recherche
                var medicines = medicineDAO.GetAll();
                comboBoxSearchMedicine.DataSource = medicines;
                comboBoxSearchMedicine.DisplayMember = "Name";
                comboBoxSearchMedicine.ValueMember = "Id_medicine";

                // Initialiser les dates par défaut
                dateTimePickerStartDate.Value = DateTime.Now.AddMonths(-6);
                dateTimePickerEndDate.Value = DateTime.Now;

                // Désactiver les dates par défaut
                dateTimePickerStartDate.Enabled = false;
                dateTimePickerEndDate.Enabled = false;

                // Cacher le TextBox au départ
                textBoxSearchValue.Visible = false;
                comboBoxSearchMedicine.Visible = true;

                // Initialiser le type de recherche
                if (comboBoxSearchType.Items.Count > 0)
                    comboBoxSearchType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'initialisation de la recherche : {ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Gère le changement de type de recherche
        /// Affiche ComboBox ou TextBox selon le type choisi
        /// </summary>
        private void comboBoxSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSearchType.SelectedItem == null) return;

            string searchType = comboBoxSearchType.SelectedItem.ToString();

            switch (searchType)
            {
                case "Par Médicament":
                    // Afficher la ComboBox des médicaments
                    comboBoxSearchMedicine.Visible = true;
                    textBoxSearchValue.Visible = false;
                    break;

                case "Par Molécule":
                case "Par Nom Médicament":
                    // Afficher le TextBox pour saisie libre
                    comboBoxSearchMedicine.Visible = false;
                    textBoxSearchValue.Visible = true;
                    textBoxSearchValue.Clear();
                    break;
            }
        }

        /// <summary>
        /// Active/Désactive les DateTimePicker selon la CheckBox
        /// </summary>
        private void checkBoxUsePeriod_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerStartDate.Enabled = checkBoxUsePeriod.Checked;
            dateTimePickerEndDate.Enabled = checkBoxUsePeriod.Checked;
        }

        /// <summary>
        /// Effectue la recherche selon les critères choisis
        /// </summary>
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // Réinitialiser les résultats
                currentSearchResults = new List<PrescriptionSearchResult>();

                // Récupérer les dates si période activée
                DateTime? startDate = checkBoxUsePeriod.Checked ? (DateTime?)dateTimePickerStartDate.Value : null;
                DateTime? endDate = checkBoxUsePeriod.Checked ? (DateTime?)dateTimePickerEndDate.Value : null;

                // Récupérer le type de recherche
                string searchType = comboBoxSearchType.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(searchType))
                {
                    MessageBox.Show("Veuillez sélectionner un type de recherche.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Effectuer la recherche selon le type
                switch (searchType)
                {
                    case "Par Médicament":
                        if (comboBoxSearchMedicine.SelectedValue == null)
                        {
                            MessageBox.Show("Veuillez sélectionner un médicament.", "Validation",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        int medicineId = (int)comboBoxSearchMedicine.SelectedValue;
                        currentSearchResults = searchDAO.SearchByMedicine(medicineId, startDate, endDate);
                        break;

                    case "Par Molécule":
                        if (string.IsNullOrWhiteSpace(textBoxSearchValue.Text))
                        {
                            MessageBox.Show("Veuillez entrer une molécule à rechercher.", "Validation",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        currentSearchResults = searchDAO.SearchByMolecule(textBoxSearchValue.Text.Trim(), startDate, endDate);
                        break;

                    case "Par Nom Médicament":
                        if (string.IsNullOrWhiteSpace(textBoxSearchValue.Text))
                        {
                            MessageBox.Show("Veuillez entrer un nom de médicament à rechercher.", "Validation",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        currentSearchResults = searchDAO.SearchByMedicineName(textBoxSearchValue.Text.Trim(), startDate, endDate);
                        break;
                }

                // Afficher les résultats
                DisplaySearchResults();

                // Calculer et afficher les statistiques
                UpdateStatistics();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la recherche : {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Affiche les résultats de recherche dans le DataGridView
        /// </summary>
        private void DisplaySearchResults()
        {
            if (currentSearchResults == null || currentSearchResults.Count == 0)
            {
                dataGridViewSearchResults.DataSource = null;
                MessageBox.Show("Aucun résultat trouvé.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Créer une liste formatée pour l'affichage
            var displayList = currentSearchResults.Select(r => new
            {
                Patient = r.GetFullPatientName(),
                Age = r.PatientAge,
                Sexe = r.PatientGender,
                Médicament = r.MedicineName,
                Molécule = r.Molecule,
                Dosage = r.Dosage + " mg",
                Quantité = r.Quantity,
                Date_Prescription = r.PrescriptionValidity.ToString("dd/MM/yyyy"),
                Valide = r.IsValid() ? "Oui" : "Non"
            }).ToList();

            dataGridViewSearchResults.DataSource = displayList;
            dataGridViewSearchResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            MessageBox.Show($"{currentSearchResults.Count} résultat(s) trouvé(s).", "Succès",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Met à jour les statistiques affichées
        /// </summary>
        private void UpdateStatistics()
        {
            if (currentSearchResults == null || currentSearchResults.Count == 0)
            {
                labelStatsPatients.Text = "Patients : 0";
                labelStatsPrescriptions.Text = "Prescriptions : 0";
                labelStatsQuantity.Text = "Quantité totale : 0";
                return;
            }

            // Calculer les statistiques
            int uniquePatients = currentSearchResults.Select(r => r.Id_patient).Distinct().Count();
            int totalPrescriptions = currentSearchResults.Select(r => r.Id_prescription).Distinct().Count();
            int totalQuantity = currentSearchResults.Sum(r => r.Quantity);

            // Afficher
            labelStatsPatients.Text = $"Patients : {uniquePatients}";
            labelStatsPrescriptions.Text = $"Prescriptions : {totalPrescriptions}";
            labelStatsQuantity.Text = $"Quantité totale : {totalQuantity}";
        }

        /// <summary>
        /// Efface tous les champs de recherche et les résultats
        /// </summary>
        private void buttonClearSearch_Click(object sender, EventArgs e)
        {
            // Réinitialiser les contrôles
            if (comboBoxSearchType.Items.Count > 0)
                comboBoxSearchType.SelectedIndex = 0;

            if (comboBoxSearchMedicine.Items.Count > 0)
                comboBoxSearchMedicine.SelectedIndex = 0;

            textBoxSearchValue.Clear();
            checkBoxUsePeriod.Checked = false;
            dateTimePickerStartDate.Value = DateTime.Now.AddMonths(-6);
            dateTimePickerEndDate.Value = DateTime.Now;

            // Effacer les résultats
            currentSearchResults = null;
            dataGridViewSearchResults.DataSource = null;

            // Réinitialiser les stats
            labelStatsPatients.Text = "Patients : 0";
            labelStatsPrescriptions.Text = "Prescriptions : 0";
            labelStatsQuantity.Text = "Quantité totale : 0";
        }

        /// <summary>
        /// Exporte les résultats de recherche en CSV
        /// </summary>
        private void buttonExportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentSearchResults == null || currentSearchResults.Count == 0)
                {
                    MessageBox.Show("Aucun résultat à exporter.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Ouvrir la boîte de dialogue pour choisir l'emplacement
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Fichiers CSV (*.csv)|*.csv",
                    FileName = $"Recherche_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (System.IO.StreamWriter writer = new System.IO.StreamWriter(saveFileDialog.FileName, false, System.Text.Encoding.UTF8))
                    {
                        // En-têtes
                        writer.WriteLine("Patient;Age;Sexe;Médicament;Molécule;Dosage (mg);Quantité;Date Prescription;Valide");

                        // Données
                        foreach (var result in currentSearchResults)
                        {
                            writer.WriteLine(
                                $"{result.GetFullPatientName()};" +
                                $"{result.PatientAge};" +
                                $"{result.PatientGender};" +
                                $"{result.MedicineName};" +
                                $"{result.Molecule};" +
                                $"{result.Dosage};" +
                                $"{result.Quantity};" +
                                $"{result.PrescriptionValidity:dd/MM/yyyy};" +
                                $"{(result.IsValid() ? "Oui" : "Non")}"
                            );
                        }
                    }

                    MessageBox.Show($"Export réussi !\n{currentSearchResults.Count} ligne(s) exportée(s).", "Succès",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Demander si on veut ouvrir le fichier
                    var openResult = MessageBox.Show("Voulez-vous ouvrir le fichier ?", "Ouverture",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (openResult == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(saveFileDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'export CSV : {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Exporte les résultats de recherche en PDF
        /// </summary>
        private void buttonExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentSearchResults == null || currentSearchResults.Count == 0)
                {
                    MessageBox.Show("Aucun résultat à exporter.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Ouvrir la boîte de dialogue pour choisir l'emplacement
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Fichiers PDF (*.pdf)|*.pdf",
                    FileName = $"Recherche_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // TODO: Implémenter l'export PDF
                    // Tu peux utiliser iText7 comme pour les prescriptions
                    // Ou créer une nouvelle classe ExporterSearchResultsPDF

                    MessageBox.Show("Fonctionnalité d'export PDF à implémenter avec iText7.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // EXEMPLE DE CODE (à adapter selon ta librairie PDF) :
                    // ExporterPDF.ExportSearchResults(currentSearchResults, saveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'export PDF : {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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