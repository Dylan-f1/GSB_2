using System;
using System.Collections.Generic;
using GSB_2.Models;
using GSB2.DAO;
using MySql.Data.MySqlClient;

namespace GSB_2.DAO
{
    public class AppartientDAO
    {
        private readonly Database db = new Database();

        // Ajouter un médicament à une prescription avec quantité
        public bool addMedicineToPrescrition(int id_prescription, int id_medicine, int quantity)
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"INSERT INTO appartient (id_prescription, id_medicine, quantity)
                                            VALUES (@id_prescription, @id_medicine, @quantity)";

                    myCommand.Parameters.AddWithValue("@id_prescription", id_prescription);
                    myCommand.Parameters.AddWithValue("@id_medicine", id_medicine);
                    myCommand.Parameters.AddWithValue("@quantity", quantity);

                    myCommand.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de l'ajout du médicament à la prescription : " + ex.Message);
                    return false;
                }
            }
        }

        // Supprimer un médicament d'une prescription
        public bool removeMedicineFromPrescription(int id_prescription, int id_medicine)
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"DELETE FROM appartient 
                                            WHERE id_prescription = @id_prescription 
                                            AND id_medicine = @id_medicine";

                    myCommand.Parameters.AddWithValue("@id_prescription", id_prescription);
                    myCommand.Parameters.AddWithValue("@id_medicine", id_medicine);

                    int rowsAffected = myCommand.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la suppression du médicament de la prescription : " + ex.Message);
                    return false;
                }
            }
        }

        // Modifier la quantité d'un médicament dans une prescription
        public bool updateMedicineQuantity(int id_prescription, int id_medicine, int quantity)
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"UPDATE appartient 
                                            SET quantity = @quantity
                                            WHERE id_prescription = @id_prescription 
                                            AND id_medicine = @id_medicine";

                    myCommand.Parameters.AddWithValue("@quantity", quantity);
                    myCommand.Parameters.AddWithValue("@id_prescription", id_prescription);
                    myCommand.Parameters.AddWithValue("@id_medicine", id_medicine);

                    int rowsAffected = myCommand.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la modification de la quantité : " + ex.Message);
                    return false;
                }
            }
        }

        // Supprimer tous les médicaments d'une prescription (utile lors de la suppression de la prescription)
        public bool removeAllMedicinesFromPrescription(int id_prescription)
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"DELETE FROM appartient 
                                            WHERE id_prescription = @id_prescription";

                    myCommand.Parameters.AddWithValue("@id_prescription", id_prescription);

                    myCommand.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la suppression des médicaments : " + ex.Message);
                    return false;
                }
            }
        }

        // Récupérer tous les médicaments d'une prescription avec leurs quantités
        public List<Medicine> getMedicinesByPrescriptionId(int id_prescription)
        {
            List<Medicine> medicines = new List<Medicine>();

            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"SELECT m.id_medicine, m.id_users, m.name, m.description, m.molecule, m.dosage, a.quantity
                                            FROM medicine m
                                            INNER JOIN appartient a ON m.id_medicine = a.id_medicine
                                            WHERE a.id_prescription = @id_prescription";

                    myCommand.Parameters.AddWithValue("@id_prescription", id_prescription);

                    using (MySqlDataReader reader = myCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Medicine medicine = new Medicine(
                                reader.GetInt32("id_medicine"),
                                reader.GetInt32("id_users"),
                                reader.GetString("dosage"),
                                reader.GetString("name"),
                                reader.GetString("description"),
                                reader.GetString("molecule")
                            );
                            // Note: Vous devrez peut-être ajouter une propriété Quantity à votre classe Medicine
                            // ou créer une classe wrapper pour stocker la quantité
                            medicines.Add(medicine);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la récupération des médicaments : " + ex.Message);
                }
            }

            return medicines;
        }

        // Récupérer la quantité d'un médicament spécifique dans une prescription
        public int getMedicineQuantity(int id_prescription, int id_medicine)
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"SELECT quantity 
                                            FROM appartient 
                                            WHERE id_prescription = @id_prescription 
                                            AND id_medicine = @id_medicine";

                    myCommand.Parameters.AddWithValue("@id_prescription", id_prescription);
                    myCommand.Parameters.AddWithValue("@id_medicine", id_medicine);

                    object result = myCommand.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la récupération de la quantité : " + ex.Message);
                    return 0;
                }
            }
        }

        // Vérifier si un médicament est déjà dans une prescription (pour éviter les doublons)
        public bool isMedicineInPrescription(int id_prescription, int id_medicine)
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"SELECT COUNT(*) 
                                            FROM appartient 
                                            WHERE id_prescription = @id_prescription 
                                            AND id_medicine = @id_medicine";

                    myCommand.Parameters.AddWithValue("@id_prescription", id_prescription);
                    myCommand.Parameters.AddWithValue("@id_medicine", id_medicine);

                    int count = Convert.ToInt32(myCommand.ExecuteScalar());
                    return count > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la vérification : " + ex.Message);
                    return false;
                }
            }
        }

        // Récupérer le nombre de médicaments dans une prescription
        public int getMedicineCountByPrescriptionId(int id_prescription)
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"SELECT COUNT(*) 
                                            FROM appartient 
                                            WHERE id_prescription = @id_prescription";

                    myCommand.Parameters.AddWithValue("@id_prescription", id_prescription);

                    return Convert.ToInt32(myCommand.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors du comptage : " + ex.Message);
                    return 0;
                }
            }
        }

        // Récupérer la quantité totale de tous les médicaments d'une prescription
        public int getTotalQuantityByPrescriptionId(int id_prescription)
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand myCommand = new MySqlCommand();
                    myCommand.Connection = connection;
                    myCommand.CommandText = @"SELECT SUM(quantity) 
                                            FROM appartient 
                                            WHERE id_prescription = @id_prescription";

                    myCommand.Parameters.AddWithValue("@id_prescription", id_prescription);

                    object result = myCommand.ExecuteScalar();
                    return result != DBNull.Value && result != null ? Convert.ToInt32(result) : 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors du calcul de la quantité totale : " + ex.Message);
                    return 0;
                }
            }
        }
    }
}