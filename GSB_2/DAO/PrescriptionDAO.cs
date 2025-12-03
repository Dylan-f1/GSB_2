using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSB_2.Models;
using GSB2.DAO;
using MySql.Data.MySqlClient;

namespace GSB_2.DAO
{
    public class PrescriptionDAO
    {
        private readonly Database db = new Database();

        // CREATE - Créer une prescription
        public bool createPrescription(int id_user, int id_patient, DateTime validity, bool userRole)
        {
            if (userRole) return false; // Si True (1) alors interdit

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"INSERT INTO Prescription (id_user, id_patient, validity)
                                            VALUES (@id_user, @id_patient, @validity)";

                    myCommand.Parameters.AddWithValue("@id_user", id_user);
                    myCommand.Parameters.AddWithValue("@id_patient", id_patient);
                    myCommand.Parameters.AddWithValue("@validity", validity.ToString("yyyy-MM-dd"));

                    myCommand.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la création : " + ex.Message);
                    return false;
                }
            }
        }

        // UPDATE - Mettre à jour une prescription
        public bool updatePrescription(int id_prescription, int id_user, int id_patient, DateTime validity, bool userRole)
        {
            if (userRole) return false; // Si True (1) alors interdit

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"UPDATE Prescription 
                                            SET id_user = @id_user, 
                                                id_patient = @id_patient, 
                                                validity = @validity
                                            WHERE id_prescription = @id_prescription";

                    myCommand.Parameters.AddWithValue("@id_prescription", id_prescription);
                    myCommand.Parameters.AddWithValue("@id_user", id_user);
                    myCommand.Parameters.AddWithValue("@id_patient", id_patient);
                    myCommand.Parameters.AddWithValue("@validity", validity.ToString("yyyy-MM-dd"));

                    int rowsAffected = myCommand.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la mise à jour : " + ex.Message);
                    return false;
                }
            }
        }

        // DELETE - Supprimer une prescription
        public bool deletePrescription(int id_prescription, bool userRole)
        {
            if (userRole) return false; // Si True (1) alors interdit

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"DELETE FROM Prescription WHERE id_prescription = @id_prescription";

                    myCommand.Parameters.AddWithValue("@id_prescription", id_prescription);

                    int rowsAffected = myCommand.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la suppression : " + ex.Message);
                    return false;
                }
            }
        }

        // READ ALL - Récupérer toutes les prescriptions
        public List<Prescription> getAllPrescription()
        {
            List<Prescription> prescriptions = new List<Prescription>();

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"SELECT * FROM Prescription";

                    using (MySqlDataReader reader = myCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            prescriptions.Add(new Prescription
                            {
                                Id_prescription = reader.GetInt32("id_prescription"),
                                Id_user = reader.IsDBNull(reader.GetOrdinal("id_user")) ? 0 : reader.GetInt32("id_user"),
                                Id_patient = reader.IsDBNull(reader.GetOrdinal("id_patient")) ? 0 : reader.GetInt32("id_patient"),
                                Validity = reader.IsDBNull(reader.GetOrdinal("validity")) ? DateTime.MinValue : reader.GetDateTime("validity")
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la récupération : " + ex.Message);
                }
            }

            return prescriptions;
        }

        // READ - Récupérer une prescription par son ID
        public Prescription getPrescriptionById(int id_prescription)
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"SELECT * FROM Prescription WHERE id_prescription = @id_prescription";

                    myCommand.Parameters.AddWithValue("@id_prescription", id_prescription);

                    using (MySqlDataReader reader = myCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Prescription
                            {
                                Id_prescription = reader.GetInt32("id_prescription"),
                                Id_user = reader.IsDBNull(reader.GetOrdinal("id_user")) ? 0 : reader.GetInt32("id_user"),
                                Id_patient = reader.IsDBNull(reader.GetOrdinal("id_patient")) ? 0 : reader.GetInt32("id_patient"),
                                Validity = reader.IsDBNull(reader.GetOrdinal("validity")) ? DateTime.MinValue : reader.GetDateTime("validity")
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la récupération : " + ex.Message);
                }
            }

            return null;
        }

        // READ - Récupérer les prescriptions d'un patient
        public List<Prescription> getPrescriptionByPatientId(int id_patient)
        {
            List<Prescription> prescriptions = new List<Prescription>();

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"SELECT * FROM Prescription WHERE id_patient = @id_patient";

                    myCommand.Parameters.AddWithValue("@id_patient", id_patient);

                    using (MySqlDataReader reader = myCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            prescriptions.Add(new Prescription
                            {
                                Id_prescription = reader.GetInt32("id_prescription"),
                                Id_user = reader.IsDBNull(reader.GetOrdinal("id_user")) ? 0 : reader.GetInt32("id_user"),
                                Id_patient = reader.GetInt32("id_patient"),
                                Validity = reader.IsDBNull(reader.GetOrdinal("validity")) ? DateTime.MinValue : reader.GetDateTime("validity")
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la récupération : " + ex.Message);
                }
            }

            return prescriptions;
        }

        // READ - Récupérer les prescriptions d'un utilisateur
        public List<Prescription> getPrescriptionByUserId(int id_user)
        {
            List<Prescription> prescriptions = new List<Prescription>();

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"SELECT * FROM Prescription WHERE id_user = @id_user";

                    myCommand.Parameters.AddWithValue("@id_user", id_user);

                    using (MySqlDataReader reader = myCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            prescriptions.Add(new Prescription
                            {
                                Id_prescription = reader.GetInt32("id_prescription"),
                                Id_user = reader.GetInt32("id_user"),
                                Id_patient = reader.IsDBNull(reader.GetOrdinal("id_patient")) ? 0 : reader.GetInt32("id_patient"),
                                Validity = reader.IsDBNull(reader.GetOrdinal("validity")) ? DateTime.MinValue : reader.GetDateTime("validity")
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la récupération : " + ex.Message);
                }
            }

            return prescriptions;
        }

        // Récupérer les prescriptions valides (non expirées)
        public List<Prescription> getValidPrescriptions()
        {
            List<Prescription> prescriptions = new List<Prescription>();

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"SELECT * FROM Prescription WHERE validity >= @today";

                    myCommand.Parameters.AddWithValue("@today", DateTime.Now.Date);

                    using (MySqlDataReader reader = myCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            prescriptions.Add(new Prescription
                            {
                                Id_prescription = reader.GetInt32("id_prescription"),
                                Id_user = reader.IsDBNull(reader.GetOrdinal("id_user")) ? 0 : reader.GetInt32("id_user"),
                                Id_patient = reader.IsDBNull(reader.GetOrdinal("id_patient")) ? 0 : reader.GetInt32("id_patient"),
                                Validity = reader.IsDBNull(reader.GetOrdinal("validity")) ? DateTime.MinValue : reader.GetDateTime("validity")
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la récupération : " + ex.Message);
                }
            }

            return prescriptions;
        }
    }
}