using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GSB_2.Models;
using GSB2.DAO;
using MySql.Data.MySqlClient;

namespace GSB_2.DAO
{
    public class MedicineDAO
    {
        private readonly Database db = new Database();

        // Création d'un médicament
        public bool createMedicine(int id_user, string name, string description, string molecule, int dosage, bool userRole)
        {
            if (userRole) return false; // Si True (1) alors interdit

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"INSERT INTO Medicine (id_user, name, description, molecule, dosage)
                                            VALUES (@id_user, @name, @description, @molecule, @dosage)";

                    myCommand.Parameters.AddWithValue("@id_user", id_user);
                    myCommand.Parameters.AddWithValue("@name", name);
                    myCommand.Parameters.AddWithValue("@description", description);
                    myCommand.Parameters.AddWithValue("@molecule", molecule);
                    myCommand.Parameters.AddWithValue("@dosage", dosage);

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

        // Mettre à jour un médicament
        public bool updateMedicine(int id_medicine, int id_user, string name, string description, string molecule, int dosage, bool userRole)
        {
            if (userRole) return false; // Si True (1) alors interdit

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"UPDATE Medicine 
                                            SET id_user = @id_user, 
                                                name = @name, 
                                                description = @description, 
                                                molecule = @molecule, 
                                                dosage = @dosage
                                            WHERE id_medicine = @id_medicine";

                    myCommand.Parameters.AddWithValue("@id_medicine", id_medicine);
                    myCommand.Parameters.AddWithValue("@id_user", id_user);
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
        public bool deleteMedicine(int id_medicine, bool userRole)
        {
            if (userRole) return false; // Si True (1) alors interdit

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"DELETE FROM Medicine WHERE id_medicine = @id_medicine";

                    myCommand.Parameters.AddWithValue("@id_medicine", id_medicine);

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

        // Récupérer tous les médicaments
        public List<Medicine> GetAll()
        {
            List<Medicine> listMedicine = new List<Medicine>();

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"SELECT * FROM Medicine";

                    using (MySqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        while (myReader.Read())
                        {
                            Medicine medicine = new Medicine(
                                myReader.GetInt32("id_medicine"),
                                myReader.GetInt32("id_user"),
                                myReader.GetInt32("dosage"),
                                myReader.GetString("name"),
                                myReader.GetString("description"),
                                myReader.GetString("molecule")
                            );
                            listMedicine.Add(medicine);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur: {ex.Message}", "Erreur de base de données",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return listMedicine;
        }

        // Récupérer un médicament par son ID
        public Medicine getMedicineById(int id_medicine)
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"SELECT * FROM Medicine WHERE id_medicine = @id_medicine";

                    myCommand.Parameters.AddWithValue("@id_medicine", id_medicine);

                    using (MySqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.Read())
                        {
                            return new Medicine(
                                myReader.GetInt32("id_medicine"),
                                myReader.GetInt32("id_user"),
                                myReader.GetInt32("dosage"),
                                myReader.GetString("name"),
                                myReader.GetString("description"),
                                myReader.GetString("molecule")
                            );
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

        // Rechercher des médicaments par nom
        public List<Medicine> searchMedicineByName(string name)
        {
            List<Medicine> listMedicine = new List<Medicine>();

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"SELECT * FROM Medicine WHERE name LIKE @name";

                    myCommand.Parameters.AddWithValue("@name", "%" + name + "%");

                    using (MySqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        while (myReader.Read())
                        {
                            Medicine medicine = new Medicine(
                                myReader.GetInt32("id_medicine"),
                                myReader.GetInt32("id_user"),
                                myReader.GetInt32("dosage"),
                                myReader.GetString("name"),
                                myReader.GetString("description"),
                                myReader.GetString("molecule")
                            );
                            listMedicine.Add(medicine);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la recherche : " + ex.Message);
                }
            }

            return listMedicine;
        }

        // Récupérer les médicaments d'un utilisateur spécifique
        public List<Medicine> getMedicinesByUserId(int id_user)
        {
            List<Medicine> listMedicine = new List<Medicine>();

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"SELECT * FROM Medicine WHERE id_user = @id_user";

                    myCommand.Parameters.AddWithValue("@id_user", id_user);

                    using (MySqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        while (myReader.Read())
                        {
                            Medicine medicine = new Medicine(
                                myReader.GetInt32("id_medicine"),
                                myReader.GetInt32("id_user"),
                                myReader.GetInt32("dosage"),
                                myReader.GetString("name"),
                                myReader.GetString("description"),
                                myReader.GetString("molecule")
                            );
                            listMedicine.Add(medicine);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la récupération : " + ex.Message);
                }
            }

            return listMedicine;
        }
    }
}