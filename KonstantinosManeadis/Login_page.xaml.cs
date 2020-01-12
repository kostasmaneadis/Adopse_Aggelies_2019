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

        private static String static_connectionString = Settings1.Default.connectionString;
        private static String User_Role = "guest";
        private static String User_ID = "#";
        public Login_page()
        {
            InitializeComponent();
            if (User_ID != "#")
            {
                main_login_grid.Visibility = Visibility.Hidden;
                status.Content = "You are already Logged In";
            }
        }
        
        

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                login_status.Content = "Loading...";
                MySqlConnection connection = new MySqlConnection(static_connectionString);
                connection.Open();
                MySqlCommand command = new MySqlCommand("select password,role,userid from users where username=@username", connection);
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
                            User_ID = reader["userid"].ToString();
                            login_status.Foreground = System.Windows.Media.Brushes.Green;
                            login_status.Content = "Successfully Logged In";
                        }
                        else
                        {
                            login_status.Foreground = System.Windows.Media.Brushes.Red;
                            login_status.Content = "You password is wrong, try again";
                            
                        }
                    }
                    else
                    {
                        login_status.Foreground = System.Windows.Media.Brushes.Red;
                        login_status.Content = "Sorry, no member with this Username, please try again";
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
        public static string GetUserRole()
        {
            return User_Role;
        }

        public static string GetUserID()
        {
            return User_ID;
        }
        public static void Logout()
        {
            User_Role = "guest";
            User_ID = "#";
        }
        private void username_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
