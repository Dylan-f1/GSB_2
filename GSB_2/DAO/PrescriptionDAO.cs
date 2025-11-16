using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSB_2.Models;
using GSB2.DAO;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;

namespace GSB_2.DAO
{
    internal class PrescriptionDAO
    {
        private readonly Database db = new Database();
        public bool createPrescription(int id_users, int id_patients, int quantity, DateTime validity, bool userRole)
        {
            if (userRole) return false; // Si True (1) alors interdit

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();
                        MySqlCommand myCommand = new MySqlCommand();
                        myCommand.Connection = connection;
                    myCommand.CommandText = @"INSERT INTO Prescription (id_users, id_patients, quantity, validity)
                                                                   VALUES (@id_users, @id_patients, @quantity, @validity)";
                    
                        myCommand.Parameters.AddWithValue("@id_users", id_users);
                        myCommand.Parameters.AddWithValue("@id_patients", id_patients);
                        myCommand.Parameters.AddWithValue("@quantity", quantity);
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
        public bool updatePrescription(int id_prescription, int id_users, int id_patients, int quantity, DateTime validity, bool userRole)
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
                                                                   SET id_users = @id_users, 
                                                                       id_patients = @id_patients, 
                                                                       quantity = @quantity, 
                                                                       validity = @validity
                                                                   WHERE id_prescription = @id_prescription";
                    
                        myCommand.Parameters.AddWithValue("@id_prescription", id_prescription);
                        myCommand.Parameters.AddWithValue("@id_users", id_users);
                        myCommand.Parameters.AddWithValue("@id_patients", id_patients);
                        myCommand.Parameters.AddWithValue("@quantity", quantity);
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
        public bool deletePrescription(int id_prescription, int id_users, int id_patients, int quantity, DateTime validity, bool userRole)
        {
            if (userRole) return false; // Si True (1) alors interdit

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();
                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"DELETE FROM Prescription 
                                            WHERE id_prescription = @id_prescription";
                    
                    myCommand.Parameters.AddWithValue("@id_prescription", id_prescription);
                    myCommand.Parameters.AddWithValue("@id_users", id_users);
                    myCommand.Parameters.AddWithValue("@id_patients", id_patients);
                    myCommand.Parameters.AddWithValue("@quantity", quantity);
                    myCommand.Parameters.AddWithValue("@validity", validity.ToString("yyyy-MM-dd"));

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
        public List<Prescription> getAllPrescriptions()
        {
            List<Prescription> prescriptions = new List<Prescription>();

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();
                        MySqlCommand myCommand = new MySqlCommand();
                        myCommand.Connection = connection;
                        myCommand.CommandText = @"SELECT id_prescription, id_users, id_patients, quantity, validity 
                                                                   FROM Prescription";
                    
                        using (MySqlDataReader reader = myCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                prescriptions.Add(new Prescription
                                {
                                    Id = reader.GetInt32("id_prescription"),
                                    Id_users = reader.IsDBNull(reader.GetOrdinal("id_users")) ? 0 : reader.GetInt32("id_users"),
                                    Id_patients = reader.IsDBNull(reader.GetOrdinal("id_patients")) ? 0 : reader.GetInt32("id_patients"),
                                    quantity = reader.IsDBNull(reader.GetOrdinal("quantity")) ? 0 : reader.GetInt32("quantity"),
                                    validity = reader.IsDBNull(reader.GetOrdinal("validity")) ? DateTime.MinValue : reader.GetDateTime("validity")
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
        // READ - Récupérer les prescriptions d'un patient
        public List<Prescription> getPrescriptionsByPatientId(int id_patients)
        {
            List<Prescription> prescriptions = new List<Prescription>();

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();
                        MySqlCommand myCommand = new MySqlCommand();
                        myCommand.Connection = connection;
                    myCommand.CommandText = @"SELECT id_prescription, id_users, id_patients, quantity, validity 
                                                                   FROM Prescription 
                                                                   WHERE id_patients = @id_patients";
                    
                        myCommand.Parameters.AddWithValue("@id_patients", id_patients);

                        using (MySqlDataReader reader = myCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                prescriptions.Add(new Prescription
                                {
                                    Id = reader.GetInt32("id_prescription"),
                                    Id_users = reader.IsDBNull(reader.GetOrdinal("id_users")) ? 0 : reader.GetInt32("id_users"),
                                    Id_patients = reader.GetInt32("id_patients"),
                                    quantity = reader.IsDBNull(reader.GetOrdinal("quantity")) ? 0 : reader.GetInt32("quantity"),
                                    validity = reader.IsDBNull(reader.GetOrdinal("validity")) ? DateTime.MinValue : reader.GetDateTime("validity")
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

