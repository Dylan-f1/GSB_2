using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using GSB2.DAO;
using MySql.Data.MySqlClient;
using GSB_2.Models;
using System.Data;
using System.Xml.Linq;
using System.Diagnostics.Eventing.Reader;

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

                    // create a MySQL command and set the SQL statement with parameters
                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"SELECT * FROM Users WHERE email = @email AND password = @password;";
                    myCommand.Parameters.AddWithValue("@email", email);
                    myCommand.Parameters.AddWithValue("@password", password);


                    // execute the command and read the results
                    using var myReader = myCommand.ExecuteReader();
                    {
                        if (myReader.Read())
                        {
                            int id = myReader.GetInt32("id_users");
                            string name = myReader.GetString("name");
                            string firstname = myReader.GetString("firstname");
                            bool role = myReader.GetBoolean("role");

                            return new User(id, name, firstname, role);
                        }

                        else
                        {
                            return null;
                        }
                    }
                    connection.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }

    }
}
