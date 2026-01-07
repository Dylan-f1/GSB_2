using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSB_2.Models
{
    /// <summary>
    /// Modèle pour représenter un résultat de recherche complet
    /// Contient toutes les informations nécessaires pour l'analyse et la communication
    /// </summary>
    public class PrescriptionSearchResult
    {
        // Informations Patient
        public int Id_patient { get; set; }
        public string PatientName { get; set; }
        public string PatientFirstname { get; set; }
        public int PatientAge { get; set; }
        public string PatientGender { get; set; }

        // Informations Prescription
        public int Id_prescription { get; set; }
        public DateTime PrescriptionValidity { get; set; }
        public int PrescribingUserId { get; set; }

        // Informations Médicament
        public int Id_medicine { get; set; }
        public string MedicineName { get; set; }
        public string MedicineDescription { get; set; }
        public string Molecule { get; set; }
        public int Dosage { get; set; }
        public int Quantity { get; set; }

        // Constructeur par défaut
        public PrescriptionSearchResult() { }

        // Constructeur complet
        public PrescriptionSearchResult(
            int id_patient, string patientName, string patientFirstname, int patientAge, string patientGender,
            int id_prescription, DateTime prescriptionValidity, int prescribingUserId,
            int id_medicine, string medicineName, string medicineDescription, string molecule, int dosage, int quantity)
        {
            this.Id_patient = id_patient;
            this.PatientName = patientName;
            this.PatientFirstname = patientFirstname;
            this.PatientAge = patientAge;
            this.PatientGender = patientGender;

            this.Id_prescription = id_prescription;
            this.PrescriptionValidity = prescriptionValidity;
            this.PrescribingUserId = prescribingUserId;

            this.Id_medicine = id_medicine;
            this.MedicineName = medicineName;
            this.MedicineDescription = medicineDescription;
            this.Molecule = molecule;
            this.Dosage = dosage;
            this.Quantity = quantity;
        }

        /// <summary>
        /// Retourne le nom complet du patient
        /// </summary>
        public string GetFullPatientName()
        {
            return $"{PatientFirstname} {PatientName}";
        }

        /// <summary>
        /// Vérifie si la prescription est encore valide
        /// </summary>
        public bool IsValid()
        {
            return PrescriptionValidity >= DateTime.Now.Date;
        }

        /// <summary>
        /// Retourne une description formatée du médicament
        /// </summary>
        public string GetMedicineDetails()
        {
            return $"{MedicineName} ({Molecule}) - {Dosage}mg - Quantité: {Quantity}";
        }
    }
}