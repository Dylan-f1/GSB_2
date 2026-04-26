using System.Configuration;
using MySql.Data.MySqlClient;

namespace GSB2.DAO
{
    public class Database
    {
        private readonly string myConnectionString =
            ConfigurationManager.ConnectionStrings["GSB2"].ConnectionString;

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(myConnectionString);
        }
    }
}