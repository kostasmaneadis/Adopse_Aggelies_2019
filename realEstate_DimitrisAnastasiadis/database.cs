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
    }
}
