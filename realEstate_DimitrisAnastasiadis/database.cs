using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace realEstate_DimitrisAnastasiadis
{
    class database
    {
        private static String conString = Settings1.Default.connectionString;

        public static void insertQuery(String query)
        {
            try
            {
                MySqlConnection con = new MySqlConnection(conString);
                con.Open();
                MySqlCommand command = new MySqlCommand(query, con);
                command.ExecuteNonQuery();
                con.Close();
            }catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public static long insertAutoIncrement(String query)
        {
            long id = -1;
            try
            {
                MySqlConnection con = new MySqlConnection(conString);
                con.Open();
                MySqlCommand command = new MySqlCommand(query, con);
                command.ExecuteNonQuery();
                id = command.LastInsertedId;
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return id;
        }

        public static List<String> selectQuery(String query)
        {
            List<String> data = new List<string>();
            try
            {
                MySqlConnection con = new MySqlConnection(conString);
                con.Open();
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    data.Add(reader.GetString(0));
                reader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return data;
        }
    }
}
