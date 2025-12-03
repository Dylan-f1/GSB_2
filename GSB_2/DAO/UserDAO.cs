using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GSB2.DAO;
using MySql.Data.MySqlClient;
using GSB_2.Models;

namespace GSB_2.DAO
{
    public class UserDAO
    {
        private readonly Database db = new Database();

        // Login - Connexion d'un utilisateur
        public User Login(string email, string password)
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"SELECT * FROM Users WHERE email = @email AND password = SHA2(@password, 256)";

                    myCommand.Parameters.AddWithValue("@email", email);
                    myCommand.Parameters.AddWithValue("@password", password);

                    using (MySqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.Read())
                        {
                            int id = myReader.GetInt32("id_user");
                            string firstname = myReader.GetString("firstname");
                            string name = myReader.GetString("name");
                            bool role = myReader.GetBoolean("role");

                            return new User(id, firstname, name, email, password, role);
                        }
                        else
                        {
                            MessageBox.Show("Aucun utilisateur trouvé avec ces identifiants.", "Information");
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la connexion :\n{ex.Message}", "Erreur",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        // READ ALL - Récupérer tous les utilisateurs
        public List<User> GetAll()
        {
            List<User> listUser = new List<User>();

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"SELECT * FROM Users";

                    using (MySqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        while (myReader.Read())
                        {
                            User user = new User(
                                myReader.GetInt32("id_user"),
                                myReader.GetString("firstname"),
                                myReader.GetString("name"),
                                myReader.GetString("email"),
                                myReader.GetString("password"),
                                myReader.GetBoolean("role")
                            );
                            listUser.Add(user);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de la récupération : {ex.Message}");
                    MessageBox.Show($"Erreur: {ex.Message}", "Erreur de base de données",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return listUser;
        }

        // CREATE - Ajouter un utilisateur
        public bool Add(User user)
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"INSERT INTO Users (firstname, name, email, password, role) 
                                            VALUES (@firstname, @name, @email, SHA2(@password, 256), @role)";

                    myCommand.Parameters.AddWithValue("@firstname", user.Firstname);
                    myCommand.Parameters.AddWithValue("@name", user.Name);
                    myCommand.Parameters.AddWithValue("@email", user.Email);
                    myCommand.Parameters.AddWithValue("@password", user.Password);
                    myCommand.Parameters.AddWithValue("@role", user.Role);

                    int rowsAffected = myCommand.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de l'ajout : {ex.Message}");
                    MessageBox.Show($"Erreur lors de l'ajout: {ex.Message}", "Erreur de base de données",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        // UPDATE - Mettre à jour un utilisateur
        public bool Update(User user)
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"UPDATE Users 
                                            SET firstname = @firstname, 
                                                name = @name, 
                                                email = @email,
                                                password = SHA2(@password, 256), 
                                                role = @role 
                                            WHERE id_user = @id";

                    myCommand.Parameters.AddWithValue("@id", user.Id_user);
                    myCommand.Parameters.AddWithValue("@firstname", user.Firstname);
                    myCommand.Parameters.AddWithValue("@name", user.Name);
                    myCommand.Parameters.AddWithValue("@email", user.Email);
                    myCommand.Parameters.AddWithValue("@password", user.Password);
                    myCommand.Parameters.AddWithValue("@role", user.Role);

                    int rowsAffected = myCommand.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de la modification : {ex.Message}");
                    MessageBox.Show($"Erreur lors de la modification: {ex.Message}", "Erreur de base de données",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        // DELETE - Supprimer un utilisateur
        public bool Delete(int userId)
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"DELETE FROM Users WHERE id_user = @id";

                    myCommand.Parameters.AddWithValue("@id", userId);

                    int rowsAffected = myCommand.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de la suppression : {ex.Message}");
                    MessageBox.Show($"Erreur lors de la suppression: {ex.Message}", "Erreur de base de données",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        // READ - Récupérer un utilisateur par son ID
        public User GetById(int userId)
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"SELECT * FROM Users WHERE id_user = @id";

                    myCommand.Parameters.AddWithValue("@id", userId);

                    using (MySqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.Read())
                        {
                            return new User(
                                myReader.GetInt32("id_user"),
                                myReader.GetString("firstname"),
                                myReader.GetString("name"),
                                myReader.GetString("email"),
                                myReader.GetString("password"),
                                myReader.GetBoolean("role")
                            );
                        }
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de la récupération : {ex.Message}");
                    MessageBox.Show($"Erreur: {ex.Message}", "Erreur de base de données",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        // Récupérer un utilisateur par son email
        public User GetByEmail(string email)
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"SELECT * FROM Users WHERE email = @email";

                    myCommand.Parameters.AddWithValue("@email", email);

                    using (MySqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.Read())
                        {
                            return new User(
                                myReader.GetInt32("id_user"),
                                myReader.GetString("firstname"),
                                myReader.GetString("name"),
                                myReader.GetString("email"),
                                myReader.GetString("password"),
                                myReader.GetBoolean("role")
                            );
                        }
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de la récupération : {ex.Message}");
                    return null;
                }
            }
        }

        // Vérifier si un email existe déjà
        public bool EmailExists(string email)
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"SELECT COUNT(*) FROM Users WHERE email = @email";

                    myCommand.Parameters.AddWithValue("@email", email);

                    int count = Convert.ToInt32(myCommand.ExecuteScalar());
                    return count > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de la vérification : {ex.Message}");
                    return false;
                }
            }
        }
    }
}