using System;
using System.Windows;
using MySql.Data.MySqlClient;
namespace KonstantinosManeadis
{
    /// <summary>
    /// Interaction logic for EditUserProfile.xaml
    /// </summary>
    public partial class EditUserProfile : Window
    {
        private static String static_connectionString = Settings1.Default.connectionString;
        public EditUserProfile()
        {
            InitializeComponent();
            
            try
            {
                MySqlConnection connection = new MySqlConnection(static_connectionString);
                connection.Open();
                MySqlCommand command = new MySqlCommand("select username,firstname,lastname,email from users where id=@id", connection);
                string id = Login_page.GetUserID();
                command.Parameters.AddWithValue("id", id);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        username_textbox.Text = reader["username"].ToString();
                        firstname_textbox.Text= reader["firstname"].ToString();
                        lastname_textbox.Text = reader["lastname"].ToString();                 
                        email_textbox.Text = reader["email"].ToString();
                    }
                    else
                    {
                        username_textbox.Text = "You must Login to Continue to this page";
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //test register to db with mysql client
                MySqlConnection connection = new MySqlConnection(static_connectionString);
                connection.Open();
                MySqlCommand command = new MySqlCommand("UPDATE `users` SET `username`=@username, `firstname`=@firstname, `lastname`=@lastname, `email`=@email Where `id`=@id ", connection);

                string username = username_textbox.Text;
                command.Parameters.AddWithValue("username", username);

                string firstname = firstname_textbox.Text;
                command.Parameters.AddWithValue("firstname", firstname);

                string lastname = lastname_textbox.Text;
                command.Parameters.AddWithValue("lastname", lastname);

                string email = email_textbox.Text;
                command.Parameters.AddWithValue("email", email);

                string id = KonstantinosManeadis.Login_page.GetUserID();
                command.Parameters.AddWithValue("id", id);

                command.ExecuteNonQuery();
                connection.Close();
                SaveButton.Visibility = Visibility.Hidden;
                CancelButton.Content = "Exit";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
