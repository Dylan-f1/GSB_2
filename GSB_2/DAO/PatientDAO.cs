using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSB2.DAO;
using MySql.Data.MySqlClient;
using GSB_2.Models;

namespace GSB_2.DAO
{
    public class PatientDAO
    {
        private readonly Database db = new Database();

        // Créer un nouveau patient
        public bool createPatient(int id_user, string name, int age, string firstname, string gender, int? id_regime, bool userRole)
        {
            if (userRole) return false; // Si True (1) alors interdit

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"INSERT INTO Patients (id_user, name, age, firstname, gender, id_regime)
                                            VALUES (@id_user, @name, @age, @firstname, @gender, @id_regime)";

                    myCommand.Parameters.AddWithValue("@id_user", id_user);
                    myCommand.Parameters.AddWithValue("@name", name);
                    myCommand.Parameters.AddWithValue("@age", age);
                    myCommand.Parameters.AddWithValue("@firstname", firstname);
                    myCommand.Parameters.AddWithValue("@gender", gender);
                    // Si id_regime est null (aucun régime), on insère DBNull.Value → NULL en base
                    myCommand.Parameters.AddWithValue("@id_regime", (object)id_regime ?? DBNull.Value);

                    myCommand.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la création du patient : " + ex.Message);
                    return false;
                }
            }
        }

        // Mettre à jour un patient
        public bool updatePatient(int id_patient, int id_user, string name, int age, string firstname, string gender, bool userRole)
        {
            if (userRole) return false; // Si True (1) alors interdit

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"UPDATE Patients 
                                            SET id_user = @id_user, 
                                                name = @name, 
                                                age = @age, 
                                                firstname = @firstname, 
                                                gender = @gender
                                            WHERE id_patient = @id_patient";

                    myCommand.Parameters.AddWithValue("@id_patient", id_patient);
                    myCommand.Parameters.AddWithValue("@id_user", id_user);
                    myCommand.Parameters.AddWithValue("@name", name);
                    myCommand.Parameters.AddWithValue("@age", age);
                    myCommand.Parameters.AddWithValue("@firstname", firstname);
                    myCommand.Parameters.AddWithValue("@gender", gender);

                    int rowsAffected = myCommand.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la mise à jour du patient : " + ex.Message);
                    return false;
                }
            }
        }

        // Supprimer un patient
        public bool deletePatient(int id_patient, bool userRole)
        {
            if (userRole) return false; // Si True (1) alors interdit

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"DELETE FROM Patients WHERE id_patient = @id_patient";

                    myCommand.Parameters.AddWithValue("@id_patient", id_patient);

                    int rowsAffected = myCommand.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la suppression du patient : " + ex.Message);
                    return false;
                }
            }
        }

        // Récupérer tous les patients
        public List<Patient> GetAll()
        {
            List<Patient> patients = new List<Patient>();

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    // LEFT JOIN pour récupérer le libellé du régime en une seule requête
                    // LEFT JOIN et non INNER JOIN : les patients sans régime (id_regime NULL) restent dans la liste
                    myCommand.CommandText = @"SELECT p.id_patient, p.id_user, p.name, p.firstname, p.age, p.gender,
                                                     p.id_regime, r.label AS regime_label
                                            FROM Patients p
                                            LEFT JOIN Regimes r ON p.id_regime = r.id_regime";

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
                                reader.GetString("gender"),
                                // IsDBNull vérifie si id_regime est NULL en base (patient sans régime)
                                reader.IsDBNull(reader.GetOrdinal("id_regime")) ? (int?)null : reader.GetInt32("id_regime"),
                                reader.IsDBNull(reader.GetOrdinal("regime_label")) ? null : reader.GetString("regime_label")
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

        // Récupérer un patient par son ID
        public Patient GetPatientById(int id_patient)
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    // LEFT JOIN pour récupérer le libellé du régime en une seule requête
                    myCommand.CommandText = @"SELECT p.id_patient, p.id_user, p.name, p.firstname, p.age, p.gender,
                                                     p.id_regime, r.label AS regime_label
                                            FROM Patients p
                                            LEFT JOIN Regimes r ON p.id_regime = r.id_regime
                                            WHERE p.id_patient = @id_patient";

                    myCommand.Parameters.AddWithValue("@id_patient", id_patient);

                    using (MySqlDataReader reader = myCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Patient(
                                reader.GetInt32("id_patient"),
                                reader.GetInt32("id_user"),
                                reader.GetString("name"),
                                reader.GetString("firstname"),
                                reader.GetInt32("age"),
                                reader.GetString("gender"),
                                reader.IsDBNull(reader.GetOrdinal("id_regime")) ? (int?)null : reader.GetInt32("id_regime"),
                                reader.IsDBNull(reader.GetOrdinal("regime_label")) ? null : reader.GetString("regime_label")
                            );
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la récupération du patient : " + ex.Message);
                }
            }

            return null;
        }

        // Rechercher des patients par nom
        public List<Patient> searchPatientByName(string name)
        {
            List<Patient> patients = new List<Patient>();

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    // LEFT JOIN pour récupérer le libellé du régime en une seule requête
                    myCommand.CommandText = @"SELECT p.id_patient, p.id_user, p.name, p.firstname, p.age, p.gender,
                                                     p.id_regime, r.label AS regime_label
                                            FROM Patients p
                                            LEFT JOIN Regimes r ON p.id_regime = r.id_regime
                                            WHERE p.name LIKE @name";

                    myCommand.Parameters.AddWithValue("@name", "%" + name + "%");

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
                                reader.GetString("gender"),
                                reader.IsDBNull(reader.GetOrdinal("id_regime")) ? (int?)null : reader.GetInt32("id_regime"),
                                reader.IsDBNull(reader.GetOrdinal("regime_label")) ? null : reader.GetString("regime_label")
                            );
                            patients.Add(patient);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la recherche de patients : " + ex.Message);
                }
            }

            return patients;
        }

        // Récupérer les patients d'un utilisateur spécifique
        public List<Patient> getPatientsByUserId(int id_user)
        {
            List<Patient> patients = new List<Patient>();

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    // LEFT JOIN pour récupérer le libellé du régime en une seule requête
                    myCommand.CommandText = @"SELECT p.id_patient, p.id_user, p.name, p.firstname, p.age, p.gender,
                                                     p.id_regime, r.label AS regime_label
                                            FROM Patients p
                                            LEFT JOIN Regimes r ON p.id_regime = r.id_regime
                                            WHERE p.id_user = @id_user";

                    myCommand.Parameters.AddWithValue("@id_user", id_user);

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
                                reader.GetString("gender"),
                                reader.IsDBNull(reader.GetOrdinal("id_regime")) ? (int?)null : reader.GetInt32("id_regime"),
                                reader.IsDBNull(reader.GetOrdinal("regime_label")) ? null : reader.GetString("regime_label")
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

        // Récupérer les patients ayant un régime alimentaire spécifique
        // INNER JOIN (et non LEFT JOIN) : on veut uniquement les patients qui ont ce régime
        // La requête est paramétrée : @id_regime protège contre les injections SQL
        public List<Patient> getPatientsByRegime(int id_regime)
        {
            List<Patient> patients = new List<Patient>();

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"SELECT p.id_patient, p.id_user, p.name, p.firstname, p.age, p.gender,
                                                     p.id_regime, r.label AS regime_label
                                            FROM Patients p
                                            INNER JOIN Regimes r ON p.id_regime = r.id_regime
                                            WHERE p.id_regime = @id_regime";

                    myCommand.Parameters.AddWithValue("@id_regime", id_regime);

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
                                reader.GetString("gender"),
                                reader.GetInt32("id_regime"),
                                reader.GetString("regime_label")
                            );
                            patients.Add(patient);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la récupération des patients par régime : " + ex.Message);
                }
            }

            return patients;
        }

        // Compter le nombre de patients
        public int getPatientCount()
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"SELECT COUNT(*) FROM Patients";

                    return Convert.ToInt32(myCommand.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors du comptage des patients : " + ex.Message);
                    return 0;
                }
            }
        }
    }
}