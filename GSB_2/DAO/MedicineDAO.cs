using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSB_2.Models;
using GSB2.DAO;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace GSB_2.DAO
{
    public class MedicineDAO
    {
        private readonly Database db = new Database();
        //crétion d'un médicament
        public bool createmedicine(int id, int id_user, string name, string description, string molecule, string dosage, bool userRole)
        {
            if (userRole) return false; //si True (1) alors interdit

            using(var connection = db.GetConnection()) 
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"INSERT INTO medicine (id_users, name, description, molecule, dosage)
                                                VALUES (@id_users, @name, @description, @molecule, @dosage)";
                    myCommand.Parameters.AddWithValue("@id_users", id_user);
                    myCommand.Parameters.AddWithValue("@name", name);
                    myCommand.Parameters.AddWithValue("@description", description);
                    myCommand.Parameters.AddWithValue("@molecule", molecule);
                    myCommand.Parameters.AddWithValue("@dosage", dosage);


                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error lors de la création" + ex.Message);
                    return false;
                }
            }
        }

        // Mettre à jour un médicament
        public bool updateMedicine(int id, int id_user, string name, string description, string molecule, string dosage, bool userRole)
        {
            if (userRole) return false; // Si True (1) alors interdit

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                        MySqlCommand myCommand = new MySqlCommand();
                        myCommand.Connection = connection;
                        myCommand.CommandText = @"UPDATE medicine 
                                                               SET id_users = @id_users, 
                                                                   name = @name, 
                                                                   description = @description, 
                                                                   molecule = @molecule, 
                                                                   dosage = @dosage
                                                               WHERE id = @id)";
                    
                        myCommand.Parameters.AddWithValue("@id", id);
                        myCommand.Parameters.AddWithValue("@id_users", id_user);
                        myCommand.Parameters.AddWithValue("@name", name);
                        myCommand.Parameters.AddWithValue("@description", description);
                        myCommand.Parameters.AddWithValue("@molecule", molecule);
                        myCommand.Parameters.AddWithValue("@dosage", dosage);

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

        // Supprimer un médicament
        public bool deleteMedicine(int id, bool userRole)
        {
            if (userRole) return false; // Si True (1) alors interdit

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();
                        MySqlCommand myCommand = new MySqlCommand();
                        myCommand.Connection = connection;
                        myCommand.CommandText = @"DELETE FROM Medicine 
                                                               WHERE id = @id)";
                        myCommand.Parameters.AddWithValue("@id", id);

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

        public List<Medicine> GetAll()
        {
            List<Medicine> listMedicine = new List<Medicine>();

            using (var connection = db.GetConnection())
            {

                try
                {
                    connection.Open();

                    using (var myCommand = new MySqlCommand(@"SELECT * FROM Medicine;", connection))
                    {

                        using (var myReader = myCommand.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                Medicine medicine = new Medicine(
                                    myReader.GetInt32("id_medicine"), 
                                    myReader.GetInt32("id_user"), 
                                    myReader.GetString("dosage"), 
                                    myReader.GetString("name"), 
                                    myReader.GetString("description"), 
                                    myReader.GetString("molecule"));
                                listMedicine.Add(medicine);
                            }
                        }
                    }

                    return listMedicine;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de la connexion : {ex.Message}");
                    return null;
                }
            }
        }
    }
}
