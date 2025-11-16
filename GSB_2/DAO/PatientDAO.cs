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

        // UPDATE - Mettre à jour un patient
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

        // DELETE - Supprimer un patient
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

    }
}
