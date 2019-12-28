using System;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
namespace KonstantinosManeadis
{
    /// <summary>
    /// Interaction logic for Register_page.xaml
    /// </summary>
    public partial class Register_page : Page
    { 
        public Register_page()
        {
            InitializeComponent();
        }
        private static String static_connectionString = Settings1.Default.connectionString;
        private void Register_Click(object sender, RoutedEventArgs e)
        {
             try
            {
                //test register to db with mysql client
                MySqlConnection connection = new MySqlConnection(static_connectionString);
                connection.Open();
                MySqlCommand command = new MySqlCommand("INSERT INTO `users` ( `username`, `password`, `firstname`, `lastname`, `birthday`, `role`, `email`) VALUES( @username, @password, @firstname, @lastname, @birthday, 'user', @email) ", connection);

                string username = username_textbox.Text;
                command.Parameters.AddWithValue("username", username);

                string password = password_textbox.Password;
                command.Parameters.AddWithValue("password", password);

                string firstname = firstname_textbox.Text;
                command.Parameters.AddWithValue("firstname", firstname);

                string lastname = lastname_textbox.Text;
                command.Parameters.AddWithValue("lastname", lastname);

                string email = email_textbox.Text;
                command.Parameters.AddWithValue("email", email);

                string birthday= birthday_textbox.Text;
                command.Parameters.AddWithValue("birthday", birthday);

                command.ExecuteNonQuery();
                connection.Close();

                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
