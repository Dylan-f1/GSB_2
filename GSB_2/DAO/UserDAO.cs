using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GSB2.DAO;
using MySql.Data.MySqlClient;
using GSB_2.Models;
using System.Data;

namespace GSB_2.DAO
{
    public class UserDAO
    {
        private readonly Database db = new Database();        
        public User Login(string email, string password)
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"SELECT * FROM Users WHERE email = @email AND password = @password;";
                    myCommand.Parameters.AddWithValue("@email", email);
                    myCommand.Parameters.AddWithValue("@password", password);

                    using (var myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.Read())
                        {
                            int id = myReader.GetInt32("id_users");
                            string name = myReader.GetString("name");
                            string firstname = myReader.GetString("firstname");
                            bool role = myReader.GetBoolean("role");

                            return new User(id, firstname, name, password, email, role);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de la connexion : {ex.Message}");
                    MessageBox.Show($"Erreur de connexion: {ex.Message}", "Erreur",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        public List<User> GetAll()
        {
            List<User> listUser = new List<User>();
            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connexion ouverte avec succès");

                    using (var myCommand = new MySqlCommand(@"SELECT * FROM Users;", connection))
                    {
                        using (var myReader = myCommand.ExecuteReader())
                        {
                            Console.WriteLine("Requête exécutée");

                            while (myReader.Read())
                            {
                                User user = new User(
                                    myReader.GetInt32("id_users"),
                                    myReader.GetString("firstname"),
                                    myReader.GetString("name"),
                                    myReader.GetString("password"),
                                    myReader.GetString("email"),
                                    myReader.GetBoolean("role"));
                                listUser.Add(user);
                            }

                            Console.WriteLine($"Nombre d'utilisateurs trouvés: {listUser.Count}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de la récupération : {ex.Message}");
                    Console.WriteLine($"StackTrace : {ex.StackTrace}");
                    MessageBox.Show($"Erreur: {ex.Message}\n\nDétails: {ex.StackTrace}",
                                   "Erreur de base de données",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);
                }
            }
            return listUser;
        }
       
        public bool Add(User user)
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    string query = @"INSERT INTO Users (firstname, name, password, email, role) 
                                   VALUES (@firstname, @name, @password, @email, @role)";

                    using (var myCommand = new MySqlCommand(query, connection))
                    {
                        myCommand.Parameters.AddWithValue("@firstname", user.Firstname);
                        myCommand.Parameters.AddWithValue("@name", user.Name);
                        myCommand.Parameters.AddWithValue("@password", user.Password);
                        myCommand.Parameters.AddWithValue("@email", user.Email);
                        myCommand.Parameters.AddWithValue("@role", user.Role);

                        int rowsAffected = myCommand.ExecuteNonQuery();
                        Console.WriteLine($"Utilisateur ajouté, lignes affectées: {rowsAffected}");
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de l'ajout : {ex.Message}");
                    MessageBox.Show($"Erreur lors de l'ajout: {ex.Message}",
                                   "Erreur de base de données",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public bool Update(User user)
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    string query = @"UPDATE Users 
                                   SET firstname = @firstname, 
                                       name = @name, 
                                       password = @password, 
                                       email = @email, 
                                       role = @role 
                                   WHERE id_users = @id";

                    using (var myCommand = new MySqlCommand(query, connection))
                    {
                        myCommand.Parameters.AddWithValue("@id", user.Id);
                        myCommand.Parameters.AddWithValue("@firstname", user.Firstname);
                        myCommand.Parameters.AddWithValue("@name", user.Name);
                        myCommand.Parameters.AddWithValue("@password", user.Password);
                        myCommand.Parameters.AddWithValue("@email", user.Email);
                        myCommand.Parameters.AddWithValue("@role", user.Role);

                        int rowsAffected = myCommand.ExecuteNonQuery();
                        Console.WriteLine($"Utilisateur modifié, lignes affectées: {rowsAffected}");
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de la modification : {ex.Message}");
                    MessageBox.Show($"Erreur lors de la modification: {ex.Message}",
                                   "Erreur de base de données",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public bool Delete(int userId)
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    string query = @"DELETE FROM Users WHERE id_users = @id";

                    using (var myCommand = new MySqlCommand(query, connection))
                    {
                        myCommand.Parameters.AddWithValue("@id", userId);

                        int rowsAffected = myCommand.ExecuteNonQuery();
                        Console.WriteLine($"Utilisateur supprimé, lignes affectées: {rowsAffected}");
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de la suppression : {ex.Message}");
                    MessageBox.Show($"Erreur lors de la suppression: {ex.Message}",
                                   "Erreur de base de données",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public User GetById(int userId)
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    string query = @"SELECT * FROM Users WHERE id_users = @id";

                    using (var myCommand = new MySqlCommand(query, connection))
                    {
                        myCommand.Parameters.AddWithValue("@id", userId);

                        using (var myReader = myCommand.ExecuteReader())
                        {
                            if (myReader.Read())
                            {
                                return new User(
                                    myReader.GetInt32("id_users"),
                                    myReader.GetString("firstname"),
                                    myReader.GetString("name"),
                                    myReader.GetString("password"),
                                    myReader.GetString("email"),
                                    myReader.GetBoolean("role"));
                            }
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de la récupération : {ex.Message}");
                    MessageBox.Show($"Erreur: {ex.Message}",
                                   "Erreur de base de données",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);
                    return null;
                }
            }
        }
    }
}