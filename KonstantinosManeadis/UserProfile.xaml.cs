using System;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
namespace KonstantinosManeadis
{
    /// <summary>
    /// Interaction logic for UserProfile.xaml
    /// </summary>
    public partial class UserProfile : Page
    {
        private static String static_connectionString = Settings1.Default.connectionString;
        public UserProfile()
        {
            InitializeComponent();
            ShowsNavigationUI = false;
            try
            {
                MySqlConnection connection = new MySqlConnection(static_connectionString);
                connection.Open();
                MySqlCommand command = new MySqlCommand("select username,firstname,lastname,birthday,email from users where id=@id", connection);
                string id = Login_page.GetUserID();
                command.Parameters.AddWithValue("id", id);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Username_label.Content = reader["username"].ToString();
                        Firstname_label.Content = reader["firstname"].ToString();
                        Lastname_label.Content = reader["lastname"].ToString();
                        Birthday_label.Content = reader["birthday"].ToString();
                        Email_label.Content = reader["email"].ToString();
                    }
                    else
                    {
                        Username_label.Content = "You must Login to Continue to this page";
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            EditUserProfile newWindows = new EditUserProfile();
            newWindows.Show();
        }
    }
}
