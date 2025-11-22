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

        public bool createPatient(int id_users, string name, int age, string firstname, string gender, bool userRole)
        {
            if (userRole) return false; // Si True (1) alors interdit

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand myCommand = new MySqlCommand(@"INSERT INTO Patients (id_users, name, age, firstname, gender)
                                                                   VALUES (@id_users, @name, @age, @firstname, @gender)", connection))
                    {
                        myCommand.Parameters.AddWithValue("@id_users", id_users);
                        myCommand.Parameters.AddWithValue("@name", name);
                        myCommand.Parameters.AddWithValue("@age", age);
                        myCommand.Parameters.AddWithValue("@firstname", firstname);
                        myCommand.Parameters.AddWithValue("@gender", gender);

                        myCommand.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la création : " + ex.Message);
                    return false;
                }
            }
        }

        public bool updatePatient(int id_patients, int id_users, string name, int age, string firstname, string gender, bool userRole)
        {
            if (userRole) return false; // Si True (1) alors interdit

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand myCommand = new MySqlCommand(@"UPDATE Patients 
                                                                   SET id_users = @id_users, 
                                                                       name = @name, 
                                                                       age = @age, 
                                                                       firstname = @firstname, 
                                                                       gender = @gender
                                                                   WHERE id_patients = @id_patients", connection))
                    {
                        myCommand.Parameters.AddWithValue("@id_patients", id_patients);
                        myCommand.Parameters.AddWithValue("@id_users", id_users);
                        myCommand.Parameters.AddWithValue("@name", name);
                        myCommand.Parameters.AddWithValue("@age", age);
                        myCommand.Parameters.AddWithValue("@firstname", firstname);
                        myCommand.Parameters.AddWithValue("@gender", gender);

                        int rowsAffected = myCommand.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la mise à jour : " + ex.Message);
                    return false;
                }
            }
        }
        public bool deletePatient(int id_patients, bool userRole)
        {
            if (userRole) return false; // Si True (1) alors interdit

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand myCommand = new MySqlCommand(@"DELETE FROM Patients 
                                                                   WHERE id_patients = @id_patients", connection))
                    {
                        myCommand.Parameters.AddWithValue("@id_patients", id_patients);

                        int rowsAffected = myCommand.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la suppression : " + ex.Message);
                    return false;
                }
            }
        }

        public List<Patient> GetAll()
        {
            List<Patient> patients = new List<Patient>();

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand myCommand = new MySqlCommand(@"SELECT id_patients, id_users, name, firstname, age, gender 
                                                               FROM Patients", connection))
                    {
                        using (MySqlDataReader reader = myCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Récupération du gender (supposé être boolean en base)
                                bool gender = false;
                                if (!reader.IsDBNull(reader.GetOrdinal("gender")))
                                {
                                    // Si c'est stocké comme string "M"/"F"
                                    var genderValue = reader.GetValue(reader.GetOrdinal("gender"));
                                    if (genderValue is string)
                                    {
                                        gender = reader.GetString("gender").ToUpper() == "M";
                                    }
                                    else if (genderValue is bool)
                                    {
                                        gender = reader.GetBoolean("gender");
                                    }
                                    else if (genderValue is int || genderValue is byte)
                                    {
                                        gender = Convert.ToInt32(genderValue) == 1;
                                    }
                                }

                                Patient patient = new Patient(
                                    reader.GetInt32("id_patients"),
                                    reader.IsDBNull(reader.GetOrdinal("id_users")) ? 0 : reader.GetInt32("id_users"),
                                    reader.IsDBNull(reader.GetOrdinal("name")) ? "" : reader.GetString("name"),
                                    reader.IsDBNull(reader.GetOrdinal("firstname")) ? "" : reader.GetString("firstname"),
                                    reader.IsDBNull(reader.GetOrdinal("age")) ? 0 : reader.GetInt32("age"),
                                    gender
                                );
                                patients.Add(patient);
                            }
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

        public Patient GetPatientById(int id_patients)
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand myCommand = new MySqlCommand(@"SELECT id_patients, id_users, name, firstname, age, gender 
                                                               FROM Patients 
                                                               WHERE id_patients = @id_patients", connection))
                    {
                        myCommand.Parameters.AddWithValue("@id_patients", id_patients);

                        using (MySqlDataReader reader = myCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Récupération du gender
                                bool gender = false;
                                if (!reader.IsDBNull(reader.GetOrdinal("gender")))
                                {
                                    var genderValue = reader.GetValue(reader.GetOrdinal("gender"));
                                    if (genderValue is string)
                                    {
                                        gender = reader.GetString("gender").ToUpper() == "M";
                                    }
                                    else if (genderValue is bool)
                                    {
                                        gender = reader.GetBoolean("gender");
                                    }
                                    else if (genderValue is int || genderValue is byte)
                                    {
                                        gender = Convert.ToInt32(genderValue) == 1;
                                    }
                                }

                                return new Patient(
                                    reader.GetInt32("id_patients"),
                                    reader.IsDBNull(reader.GetOrdinal("id_users")) ? 0 : reader.GetInt32("id_users"),
                                    reader.IsDBNull(reader.GetOrdinal("name")) ? "" : reader.GetString("name"),
                                    reader.IsDBNull(reader.GetOrdinal("firstname")) ? "" : reader.GetString("firstname"),
                                    reader.IsDBNull(reader.GetOrdinal("age")) ? 0 : reader.GetInt32("age"),
                                    gender
                                );
                            }
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

    }
}
