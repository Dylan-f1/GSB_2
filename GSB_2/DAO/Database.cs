using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using GSB_2.DAO;
using GSB_2.Models;
using MySql.Data.MySqlClient;

namespace GSB2.DAO
{
   public class Database
    {

      private readonly string myConnectionString = "server=localhost;uid=root;pwd=rootpassword;database=GSB2";

      public MySqlConnection GetConnection()
      {
        return new MySqlConnection(myConnectionString);
      }
    }
}