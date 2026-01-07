using System;
using System.Collections.Generic;
using GSB_2.Models;
using GSB2.DAO;
using MySql.Data.MySqlClient;

namespace GSB_2.DAO
{
    /// <summary>
    /// DAO spécialisé pour les recherches complexes de prescriptions
    /// Permet de rechercher par médicament, molécule et période
    /// </summary>
    public class PrescriptionSearchDAO
    {
        private readonly Database db = new Database();

        /// <summary>
        /// Recherche tous les patients ayant reçu un médicament spécifique
        /// </summary>
        /// <param name="medicineId">ID du médicament recherché</param>
        /// <param name="startDate">Date de début (optionnelle)</param>
        /// <param name="endDate">Date de fin (optionnelle)</param>
        /// <returns>Liste des résultats de recherche</returns>
        public List<PrescriptionSearchResult> SearchByMedicine(int medicineId, DateTime? startDate = null, DateTime? endDate = null)
        {
            List<PrescriptionSearchResult> results = new List<PrescriptionSearchResult>();

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;

                    // Construction de la requête SQL avec jointures
                    string query = @"
                        SELECT 
                            pat.id_patient, pat.name AS patient_name, pat.firstname AS patient_firstname, 
                            pat.age AS patient_age, pat.gender AS patient_gender,
                            pres.id_prescription, pres.validity AS prescription_validity, pres.id_user AS prescribing_user,
                            med.id_medicine, med.name AS medicine_name, med.description AS medicine_description,
                            med.molecule, med.dosage,
                            app.quantity
                        FROM Appartient app
                        INNER JOIN Prescription pres ON app.id_prescription = pres.id_prescription
                        INNER JOIN Patients pat ON pres.id_patient = pat.id_patient
                        INNER JOIN Medicine med ON app.id_medicine = med.id_medicine
                        WHERE med.id_medicine = @medicineId";

                    // Ajout du filtre de période si nécessaire
                    if (startDate.HasValue)
                    {
                        query += " AND pres.validity >= @startDate";
                    }
                    if (endDate.HasValue)
                    {
                        query += " AND pres.validity <= @endDate";
                    }

                    query += " ORDER BY pres.validity DESC, pat.name ASC";

                    myCommand.CommandText = query;
                    myCommand.Parameters.AddWithValue("@medicineId", medicineId);

                    if (startDate.HasValue)
                    {
                        myCommand.Parameters.AddWithValue("@startDate", startDate.Value.ToString("yyyy-MM-dd"));
                    }
                    if (endDate.HasValue)
                    {
                        myCommand.Parameters.AddWithValue("@endDate", endDate.Value.ToString("yyyy-MM-dd"));
                    }

                    using (MySqlDataReader reader = myCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PrescriptionSearchResult result = new PrescriptionSearchResult(
                                reader.GetInt32("id_patient"),
                                reader.GetString("patient_name"),
                                reader.GetString("patient_firstname"),
                                reader.GetInt32("patient_age"),
                                reader.GetString("patient_gender"),
                                reader.GetInt32("id_prescription"),
                                reader.GetDateTime("prescription_validity"),
                                reader.GetInt32("prescribing_user"),
                                reader.GetInt32("id_medicine"),
                                reader.GetString("medicine_name"),
                                reader.GetString("medicine_description"),
                                reader.GetString("molecule"),
                                reader.GetInt32("dosage"),
                                reader.GetInt32("quantity")
                            );
                            results.Add(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la recherche par médicament : " + ex.Message);
                }
            }

            return results;
        }

        /// <summary>
        /// Recherche tous les patients ayant reçu un médicament contenant une molécule spécifique
        /// </summary>
        /// <param name="moleculeName">Nom de la molécule (recherche LIKE)</param>
        /// <param name="startDate">Date de début (optionnelle)</param>
        /// <param name="endDate">Date de fin (optionnelle)</param>
        /// <returns>Liste des résultats de recherche</returns>
        public List<PrescriptionSearchResult> SearchByMolecule(string moleculeName, DateTime? startDate = null, DateTime? endDate = null)
        {
            List<PrescriptionSearchResult> results = new List<PrescriptionSearchResult>();

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;

                    // Construction de la requête SQL avec jointures
                    string query = @"
                        SELECT 
                            pat.id_patient, pat.name AS patient_name, pat.firstname AS patient_firstname, 
                            pat.age AS patient_age, pat.gender AS patient_gender,
                            pres.id_prescription, pres.validity AS prescription_validity, pres.id_user AS prescribing_user,
                            med.id_medicine, med.name AS medicine_name, med.description AS medicine_description,
                            med.molecule, med.dosage,
                            app.quantity
                        FROM Appartient app
                        INNER JOIN Prescription pres ON app.id_prescription = pres.id_prescription
                        INNER JOIN Patients pat ON pres.id_patient = pat.id_patient
                        INNER JOIN Medicine med ON app.id_medicine = med.id_medicine
                        WHERE med.molecule LIKE @moleculeName";

                    // Ajout du filtre de période si nécessaire
                    if (startDate.HasValue)
                    {
                        query += " AND pres.validity >= @startDate";
                    }
                    if (endDate.HasValue)
                    {
                        query += " AND pres.validity <= @endDate";
                    }

                    query += " ORDER BY pres.validity DESC, pat.name ASC";

                    myCommand.CommandText = query;
                    myCommand.Parameters.AddWithValue("@moleculeName", "%" + moleculeName + "%");

                    if (startDate.HasValue)
                    {
                        myCommand.Parameters.AddWithValue("@startDate", startDate.Value.ToString("yyyy-MM-dd"));
                    }
                    if (endDate.HasValue)
                    {
                        myCommand.Parameters.AddWithValue("@endDate", endDate.Value.ToString("yyyy-MM-dd"));
                    }

                    using (MySqlDataReader reader = myCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PrescriptionSearchResult result = new PrescriptionSearchResult(
                                reader.GetInt32("id_patient"),
                                reader.GetString("patient_name"),
                                reader.GetString("patient_firstname"),
                                reader.GetInt32("patient_age"),
                                reader.GetString("patient_gender"),
                                reader.GetInt32("id_prescription"),
                                reader.GetDateTime("prescription_validity"),
                                reader.GetInt32("prescribing_user"),
                                reader.GetInt32("id_medicine"),
                                reader.GetString("medicine_name"),
                                reader.GetString("medicine_description"),
                                reader.GetString("molecule"),
                                reader.GetInt32("dosage"),
                                reader.GetInt32("quantity")
                            );
                            results.Add(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la recherche par molécule : " + ex.Message);
                }
            }

            return results;
        }

        /// <summary>
        /// Recherche par nom de médicament (recherche LIKE)
        /// </summary>
        /// <param name="medicineName">Nom du médicament (partiel)</param>
        /// <param name="startDate">Date de début (optionnelle)</param>
        /// <param name="endDate">Date de fin (optionnelle)</param>
        /// <returns>Liste des résultats de recherche</returns>
        public List<PrescriptionSearchResult> SearchByMedicineName(string medicineName, DateTime? startDate = null, DateTime? endDate = null)
        {
            List<PrescriptionSearchResult> results = new List<PrescriptionSearchResult>();

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;

                    string query = @"
                        SELECT 
                            pat.id_patient, pat.name AS patient_name, pat.firstname AS patient_firstname, 
                            pat.age AS patient_age, pat.gender AS patient_gender,
                            pres.id_prescription, pres.validity AS prescription_validity, pres.id_user AS prescribing_user,
                            med.id_medicine, med.name AS medicine_name, med.description AS medicine_description,
                            med.molecule, med.dosage,
                            app.quantity
                        FROM Appartient app
                        INNER JOIN Prescription pres ON app.id_prescription = pres.id_prescription
                        INNER JOIN Patients pat ON pres.id_patient = pat.id_patient
                        INNER JOIN Medicine med ON app.id_medicine = med.id_medicine
                        WHERE med.name LIKE @medicineName";

                    if (startDate.HasValue)
                    {
                        query += " AND pres.validity >= @startDate";
                    }
                    if (endDate.HasValue)
                    {
                        query += " AND pres.validity <= @endDate";
                    }

                    query += " ORDER BY pres.validity DESC, pat.name ASC";

                    myCommand.CommandText = query;
                    myCommand.Parameters.AddWithValue("@medicineName", "%" + medicineName + "%");

                    if (startDate.HasValue)
                    {
                        myCommand.Parameters.AddWithValue("@startDate", startDate.Value.ToString("yyyy-MM-dd"));
                    }
                    if (endDate.HasValue)
                    {
                        myCommand.Parameters.AddWithValue("@endDate", endDate.Value.ToString("yyyy-MM-dd"));
                    }

                    using (MySqlDataReader reader = myCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PrescriptionSearchResult result = new PrescriptionSearchResult(
                                reader.GetInt32("id_patient"),
                                reader.GetString("patient_name"),
                                reader.GetString("patient_firstname"),
                                reader.GetInt32("patient_age"),
                                reader.GetString("patient_gender"),
                                reader.GetInt32("id_prescription"),
                                reader.GetDateTime("prescription_validity"),
                                reader.GetInt32("prescribing_user"),
                                reader.GetInt32("id_medicine"),
                                reader.GetString("medicine_name"),
                                reader.GetString("medicine_description"),
                                reader.GetString("molecule"),
                                reader.GetInt32("dosage"),
                                reader.GetInt32("quantity")
                            );
                            results.Add(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la recherche par nom de médicament : " + ex.Message);
                }
            }

            return results;
        }

        /// <summary>
        /// Obtenir des statistiques sur les prescriptions d'un médicament
        /// </summary>
        /// <param name="medicineId">ID du médicament</param>
        /// <param name="startDate">Date de début (optionnelle)</param>
        /// <param name="endDate">Date de fin (optionnelle)</param>
        /// <returns>Dictionnaire avec les statistiques (nb_patients, nb_prescriptions, quantite_totale)</returns>
        public Dictionary<string, int> GetMedicineStatistics(int medicineId, DateTime? startDate = null, DateTime? endDate = null)
        {
            Dictionary<string, int> stats = new Dictionary<string, int>
            {
                { "nb_patients", 0 },
                { "nb_prescriptions", 0 },
                { "quantite_totale", 0 }
            };

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;

                    string query = @"
                        SELECT 
                            COUNT(DISTINCT pat.id_patient) AS nb_patients,
                            COUNT(DISTINCT pres.id_prescription) AS nb_prescriptions,
                            SUM(app.quantity) AS quantite_totale
                        FROM Appartient app
                        INNER JOIN Prescription pres ON app.id_prescription = pres.id_prescription
                        INNER JOIN Patients pat ON pres.id_patient = pat.id_patient
                        WHERE app.id_medicine = @medicineId";

                    if (startDate.HasValue)
                    {
                        query += " AND pres.validity >= @startDate";
                    }
                    if (endDate.HasValue)
                    {
                        query += " AND pres.validity <= @endDate";
                    }

                    myCommand.CommandText = query;
                    myCommand.Parameters.AddWithValue("@medicineId", medicineId);

                    if (startDate.HasValue)
                    {
                        myCommand.Parameters.AddWithValue("@startDate", startDate.Value.ToString("yyyy-MM-dd"));
                    }
                    if (endDate.HasValue)
                    {
                        myCommand.Parameters.AddWithValue("@endDate", endDate.Value.ToString("yyyy-MM-dd"));
                    }

                    using (MySqlDataReader reader = myCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            stats["nb_patients"] = reader.IsDBNull(reader.GetOrdinal("nb_patients")) ? 0 : reader.GetInt32("nb_patients");
                            stats["nb_prescriptions"] = reader.IsDBNull(reader.GetOrdinal("nb_prescriptions")) ? 0 : reader.GetInt32("nb_prescriptions");
                            stats["quantite_totale"] = reader.IsDBNull(reader.GetOrdinal("quantite_totale")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("quantite_totale")));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors du calcul des statistiques : " + ex.Message);
                }
            }

            return stats;
        }

        /// <summary>
        /// Obtenir la liste des patients uniques ayant reçu un médicament
        /// </summary>
        /// <param name="medicineId">ID du médicament</param>
        /// <param name="startDate">Date de début (optionnelle)</param>
        /// <param name="endDate">Date de fin (optionnelle)</param>
        /// <returns>Liste des patients distincts</returns>
        public List<Patient> GetDistinctPatientsByMedicine(int medicineId, DateTime? startDate = null, DateTime? endDate = null)
        {
            List<Patient> patients = new List<Patient>();

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;

                    string query = @"
                        SELECT DISTINCT pat.id_patient, pat.id_user, pat.name, pat.firstname, pat.age, pat.gender
                        FROM Appartient app
                        INNER JOIN Prescription pres ON app.id_prescription = pres.id_prescription
                        INNER JOIN Patients pat ON pres.id_patient = pat.id_patient
                        WHERE app.id_medicine = @medicineId";

                    if (startDate.HasValue)
                    {
                        query += " AND pres.validity >= @startDate";
                    }
                    if (endDate.HasValue)
                    {
                        query += " AND pres.validity <= @endDate";
                    }

                    query += " ORDER BY pat.name ASC, pat.firstname ASC";

                    myCommand.CommandText = query;
                    myCommand.Parameters.AddWithValue("@medicineId", medicineId);

                    if (startDate.HasValue)
                    {
                        myCommand.Parameters.AddWithValue("@startDate", startDate.Value.ToString("yyyy-MM-dd"));
                    }
                    if (endDate.HasValue)
                    {
                        myCommand.Parameters.AddWithValue("@endDate", endDate.Value.ToString("yyyy-MM-dd"));
                    }

                    using (MySqlDataReader reader = myCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Patient patient = new Patient(
                                reader.GetInt32("id_patient"),
                                reader.GetInt32("id_user"),
                                reader.GetString("name"),
                                reader.GetString("firstname"),
                                reader.GetInt32("age"),
                                reader.GetString("gender")
                            );
                            patients.Add(patient);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la récupération des patients : " + ex.Message);
                }
            }

            return patients;
        }
    }
}