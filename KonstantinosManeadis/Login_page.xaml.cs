using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;


namespace KonstantinosManeadis
{
    /// <summary>
    /// Interaction logic for Login_page.xaml
    /// </summary>
    public partial class Login_page : Page
    {
        public Login_page()
        {
            InitializeComponent();
        }
        private static String static_connectionString = Settings1.Default.connectionString;
        private String User_Role = "Guest";

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(static_connectionString);
                connection.Open();
                MySqlCommand command = new MySqlCommand("select password from users where username=@username", connection);
                string username = username_textbox.Text;
                command.Parameters.AddWithValue("username", username);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string dbpass = reader["password"].ToString();
                        if (dbpass == password_textbox.Password)
                        {
                            System.Diagnostics.Debug.WriteLine(sender.ToString(), "password match with given.. we continue the login process");
                            User_Role = reader["role"].ToString();
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine(sender.ToString(), "password is wrong");
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine(sender.ToString(), "username doesnt exist to database");
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string IsLoggedIn()
        {
            string status = "No";
            if (User_Role != "Guest")
            {
                status = "Yes";
            }
            return status;
        }
        private string GetUserRole()
        {
            return User_Role;
        }
        private void username_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
