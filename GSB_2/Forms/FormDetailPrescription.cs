using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GSB_2.DAO;
using GSB_2.Models;

namespace GSB_2.Forms
{
    public class FormDetailPrescription : Form
    {
        private readonly PrescriptionDAO prescriptionDAO = new PrescriptionDAO();
        private readonly PatientDAO patientDAO = new PatientDAO();
        private readonly UserDAO userDAO = new UserDAO();
        private readonly AppartientDAO appartientDAO = new AppartientDAO();

        private Label labelTitre;
        private Label labelStatut;
        private GroupBox groupBoxPatient;
        private Label labelPatientNom;
        private Label labelPatientAge;
        private Label labelPatientGenre;
        private GroupBox groupBoxMedecin;
        private Label labelMedecinNom;
        private GroupBox groupBoxPrescription;
        private Label labelPrescriptionId;
        private Label labelPrescriptionValidite;
        private GroupBox groupBoxMedicaments;
        private DataGridView dataGridViewMedicaments;
        private Button buttonFermer;

        public FormDetailPrescription(int prescriptionId)
        {
            InitializeComponent();
            ChargerDetail(prescriptionId);
        }

        private void ChargerDetail(int prescriptionId)
        {
            try
            {
                // on récupère la prescription depuis la base via son ID
                Prescription prescription = prescriptionDAO.getPrescriptionById(prescriptionId);
                if (prescription == null)
                {
                    MessageBox.Show("Prescription introuvable.", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                // on récupère les objets liés : patient, médecin et liste des médicaments
                Patient patient = patientDAO.GetPatientById(prescription.Id_patient);
                User medecin = userDAO.GetUserById(prescription.Id_user);
                List<Medicine> medicaments = appartientDAO.getMedicinesByPrescriptionId(prescriptionId);

                labelPrescriptionId.Text = $"Prescription n°{prescription.Id_prescription}";
                labelPrescriptionValidite.Text = $"Date de validité : {prescription.Validity:dd/MM/yyyy}";

                // on utilise la propriété Statut du modèle pour afficher et coloriser le statut
                labelStatut.Text = prescription.Statut;
                if (prescription.Statut == "Valide")
                    labelStatut.ForeColor = Color.Green;
                else if (prescription.Statut == "Expire bientôt")
                    labelStatut.ForeColor = Color.Orange;
                else
                    labelStatut.ForeColor = Color.Red;

                // on vérifie que le patient existe avant d'afficher ses infos
                if (patient != null)
                {
                    labelPatientNom.Text = $"Nom : {patient.Firstname} {patient.Name}";
                    labelPatientAge.Text = $"Âge : {patient.Age} ans";
                    labelPatientGenre.Text = $"Genre : {patient.Gender}";
                }

                if (medecin != null)
                    labelMedecinNom.Text = $"Dr. {medecin.Firstname} {medecin.Name}";

                if (medicaments != null && medicaments.Count > 0)
                {
                    // pour chaque médicament on récupère la quantité prescrite dans la table Appartient
                    var displayList = new List<object>();
                    foreach (Medicine med in medicaments)
                    {
                        int quantite = appartientDAO.getMedicineQuantity(prescriptionId, med.Id_medicine);
                        // on crée un objet anonyme avec uniquement les colonnes qu'on veut afficher
                        displayList.Add(new
                        {
                            Médicament = med.Name,
                            Molécule = med.Molecule,
                            Dosage = med.Dosage + " mg",
                            Quantité = quantite,
                            Description = med.Description
                        });
                    }
                    // DataSource lie la liste au DataGridView, il génère les colonnes automatiquement
                    dataGridViewMedicaments.DataSource = displayList;
                    dataGridViewMedicaments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement : {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonFermer_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void InitializeComponent()
        {
            labelTitre = new Label();
            labelStatut = new Label();
            groupBoxPatient = new GroupBox();
            labelPatientNom = new Label();
            labelPatientAge = new Label();
            labelPatientGenre = new Label();
            groupBoxMedecin = new GroupBox();
            labelMedecinNom = new Label();
            groupBoxPrescription = new GroupBox();
            labelPrescriptionId = new Label();
            labelPrescriptionValidite = new Label();
            groupBoxMedicaments = new GroupBox();
            dataGridViewMedicaments = new DataGridView();
            buttonFermer = new Button();

            groupBoxPatient.SuspendLayout();
            groupBoxMedecin.SuspendLayout();
            groupBoxPrescription.SuspendLayout();
            groupBoxMedicaments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewMedicaments).BeginInit();
            SuspendLayout();

            labelTitre.AutoSize = true;
            labelTitre.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelTitre.Location = new Point(12, 12);
            labelTitre.Text = "Détail de l'ordonnance";

            labelStatut.AutoSize = true;
            labelStatut.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            labelStatut.Location = new Point(350, 14);

            groupBoxPrescription.Controls.Add(labelPrescriptionId);
            groupBoxPrescription.Controls.Add(labelPrescriptionValidite);
            groupBoxPrescription.Location = new Point(12, 45);
            groupBoxPrescription.Size = new Size(560, 65);
            groupBoxPrescription.Text = "Prescription";

            labelPrescriptionId.AutoSize = true;
            labelPrescriptionId.Location = new Point(10, 25);

            labelPrescriptionValidite.AutoSize = true;
            labelPrescriptionValidite.Location = new Point(200, 25);

            groupBoxPatient.Controls.Add(labelPatientNom);
            groupBoxPatient.Controls.Add(labelPatientAge);
            groupBoxPatient.Controls.Add(labelPatientGenre);
            groupBoxPatient.Location = new Point(12, 120);
            groupBoxPatient.Size = new Size(270, 100);
            groupBoxPatient.Text = "Patient";

            labelPatientNom.AutoSize = true;
            labelPatientNom.Location = new Point(10, 25);

            labelPatientAge.AutoSize = true;
            labelPatientAge.Location = new Point(10, 50);

            labelPatientGenre.AutoSize = true;
            labelPatientGenre.Location = new Point(10, 75);

            groupBoxMedecin.Controls.Add(labelMedecinNom);
            groupBoxMedecin.Location = new Point(300, 120);
            groupBoxMedecin.Size = new Size(272, 100);
            groupBoxMedecin.Text = "Médecin prescripteur";

            labelMedecinNom.AutoSize = true;
            labelMedecinNom.Location = new Point(10, 40);

            groupBoxMedicaments.Controls.Add(dataGridViewMedicaments);
            groupBoxMedicaments.Location = new Point(12, 230);
            groupBoxMedicaments.Size = new Size(560, 200);
            groupBoxMedicaments.Text = "Médicaments prescrits";

            dataGridViewMedicaments.Location = new Point(5, 20);
            dataGridViewMedicaments.Size = new Size(550, 170);
            dataGridViewMedicaments.ReadOnly = true;
            dataGridViewMedicaments.AllowUserToAddRows = false;
            dataGridViewMedicaments.RowHeadersVisible = false;

            buttonFermer.Location = new Point(460, 445);
            buttonFermer.Size = new Size(110, 32);
            buttonFermer.Text = "Fermer";
            buttonFermer.Click += buttonFermer_Click;

            ClientSize = new Size(584, 490);
            Controls.Add(labelTitre);
            Controls.Add(labelStatut);
            Controls.Add(groupBoxPrescription);
            Controls.Add(groupBoxPatient);
            Controls.Add(groupBoxMedecin);
            Controls.Add(groupBoxMedicaments);
            Controls.Add(buttonFermer);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Détail ordonnance";

            groupBoxPatient.ResumeLayout(false);
            groupBoxPatient.PerformLayout();
            groupBoxMedecin.ResumeLayout(false);
            groupBoxMedecin.PerformLayout();
            groupBoxPrescription.ResumeLayout(false);
            groupBoxPrescription.PerformLayout();
            groupBoxMedicaments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewMedicaments).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
