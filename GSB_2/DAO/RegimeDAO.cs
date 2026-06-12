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
    public class RegimeDAO
    {
        private readonly Database db = new Database();

        // Récupérer tous les régimes alimentaires
        // Cette méthode est utilisée pour alimenter les ComboBox du formulaire
        public List<Regime> GetAll()
        {
            List<Regime> regimes = new List<Regime>();

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"SELECT id_regime, label FROM Regimes";

                    using (MySqlDataReader reader = myCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Regime regime = new Regime(
                                reader.GetInt32("id_regime"),
                                reader.GetString("label")
                            );
                            regimes.Add(regime);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la récupération des régimes : " + ex.Message);
                }
            }

            return regimes;
        }
    }
}
