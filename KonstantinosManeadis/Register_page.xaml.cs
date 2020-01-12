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
            string User_ID = KonstantinosManeadis.Login_page.GetUserID(); 
            if (User_ID != "#")
            {
                main_register_grid.Visibility = Visibility.Hidden;
                status.Content = "You are already Logged In, so you don't need to Register";
            }
        }
        private static String static_connectionString = Settings1.Default.connectionString;
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            register_status.Content = "Loading...";
            try
            {
                //check if username or email exist 
                string username = username_textbox.Text;
                string email = email_textbox.Text;
                Boolean username_flag = false;
                Boolean email_flag = false;
                MySqlConnection connection = new MySqlConnection(static_connectionString);
                connection.Open();
                MySqlCommand command = new MySqlCommand("select count(username) as number from users where username=@username", connection);
                
                command.Parameters.AddWithValue("username", username);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string count = reader["number"].ToString();
                        if (count == "0" )
                        {
                            username_flag = true;
                            System.Diagnostics.Debug.WriteLine(sender.ToString(), "Username doesnt exist to any user");
                            register_status.Foreground = System.Windows.Media.Brushes.Green;
                            register_status.Content = "Loading...";
                        }
                        
                    }
                    else
                    {
                        register_status.Foreground = System.Windows.Media.Brushes.Red;
                        register_status.Content = "Sorry, something went wrong, please try again";
                    }
                }
                

                MySqlCommand command2 = new MySqlCommand("select count(email) as number from users where email=@email", connection);
                command2.Parameters.AddWithValue("email", email);
                using (MySqlDataReader reader = command2.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string count = reader["number"].ToString();
                        if (count == "0")
                        {
                            email_flag = true;
                            System.Diagnostics.Debug.WriteLine(sender.ToString(), "Email doesnt exist to any user");
                            register_status.Foreground = System.Windows.Media.Brushes.Green;
                            register_status.Content = "Loading...";
                        }
                    }
                    else
                    {
                        register_status.Foreground = System.Windows.Media.Brushes.Red;
                        register_status.Content = "Sorry, something went wrong, please try again";
                    }
                }
                

                if (!username_flag)
                {
                    register_status.Foreground = System.Windows.Media.Brushes.Red;
                    register_status.Content = "Sorry, username already exist";
                }
                if (!email_flag)
                {
                    register_status.Foreground = System.Windows.Media.Brushes.Red;
                    register_status.Content = "Sorry, this email already exist";
                }
                if (!username_flag & !email_flag)
                {
                    register_status.Foreground = System.Windows.Media.Brushes.Red;
                    register_status.Content = "Sorry,username and email address already exist";
                }
                //register new user
                if (username_flag & email_flag)
                { 
                    
                    MySqlCommand command3 = new MySqlCommand("INSERT INTO `users` ( `username`, `password`, `firstname`, `lastname`, `birthday`, `role`, `email`) VALUES( @username, @password, @firstname, @lastname, @birthday, 'user', @email) ", connection);

                    command3.Parameters.AddWithValue("username", username);

                    string password = password_textbox.Password;
                    command3.Parameters.AddWithValue("password", password);

                    string firstname = firstname_textbox.Text;
                    command3.Parameters.AddWithValue("firstname", firstname);

                    string lastname = lastname_textbox.Text;
                    command3.Parameters.AddWithValue("lastname", lastname);

                    command3.Parameters.AddWithValue("email", email);

                    string birthday = birthday_textbox.Text;
                    command3.Parameters.AddWithValue("birthday", birthday);

                    command3.ExecuteNonQuery();
                    
                    register_status.Foreground = System.Windows.Media.Brushes.Green;
                    register_status.Content = "You Successfully registered to our app";
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
