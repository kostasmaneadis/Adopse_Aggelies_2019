using System;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using System.Data;

namespace KonstantinosManeadis.Administrator
{
    /// <summary>
    /// Interaction logic for Administrator_main.xaml
    /// </summary>
    public partial class Administrator_main : Page
    {
        private static String static_connectionString = Settings1.Default.connectionString;

        public Administrator_main()
        {
            InitializeComponent();
            string LogedInUserRole = KonstantinosManeadis.Login_page.GetUserRole();
            if (LogedInUserRole == "administrator") 
            {
                try
                {
                    MySqlConnection connection = new MySqlConnection(static_connectionString);
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("select * from users", connection);
                    MySqlDataAdapter user_adapter = new MySqlDataAdapter(command);
                    DataTable dt = new DataTable("users");
                    user_adapter.Fill(dt);
                    Users_datatable.ItemsSource = dt.DefaultView;
                    user_adapter.Update(dt);
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
