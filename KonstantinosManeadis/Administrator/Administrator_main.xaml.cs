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
            if (LogedInUserRole != "administrator")
            {
                main_tab_control.Visibility = Visibility.Hidden;
                no_access_label.Content = "You must be Administrator to access this page..";
            }
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

        
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (users_tab.IsSelected)
                {
                    //users
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

                if (aggelies_tab.IsSelected)
                {
                    try
                    {
                        MySqlConnection connection = new MySqlConnection(static_connectionString);
                        connection.Open();
                        MySqlCommand command = new MySqlCommand("select * from ads", connection);
                        MySqlDataAdapter user_adapter = new MySqlDataAdapter(command);
                        DataTable dt = new DataTable("ads");
                        user_adapter.Fill(dt);
                        Ads_datatable.ItemsSource = dt.DefaultView;
                        user_adapter.Update(dt);
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                
                if (actions_tab.IsSelected)
                {

                }
                if (alerts_tab.IsSelected)
                {
                    try
                    {
                        MySqlConnection connection = new MySqlConnection(static_connectionString);
                        connection.Open();
                        MySqlCommand command = new MySqlCommand("select * from ads where ban_status='alert' ", connection);
                        MySqlDataAdapter addresses_adapter = new MySqlDataAdapter(command);
                        DataTable dt = new DataTable("ads");

                        addresses_adapter.Fill(dt);
                        alerts_datatable.ItemsSource = dt.DefaultView;
                        addresses_adapter.Update(dt);


                        connection.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
        }
        private void User_Action_Button_Click(object sender, RoutedEventArgs e)
        {
            string action = users_action.Text;
            if (action == "select")
            {
                try
                {
                    MySqlConnection connection = new MySqlConnection(static_connectionString);
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("select * from users where userid=@id", connection);
                    string id = user_id_textbox.Text;
                    command.Parameters.AddWithValue("id", id);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string output = "";
                            output = reader["username"].ToString();
                            output += " " + reader["firstname"].ToString();
                            output += " " + reader["lastname"].ToString();
                            output += " " + reader["birthday"].ToString();
                            output += " " + reader["email"].ToString();
                            users_label_status.Content = output;
                        }
                        else
                        {
                            //Username_label.Content = "You must Login to Continue to this page";
                        }
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    users_label_status.Content = "Something went wrong";
                }
            }
            else if (action == "update")
            {
                try
                {
                    MySqlConnection connection = new MySqlConnection(static_connectionString);
                    connection.Open();
                    string column = user_table_column.Text;
                    MySqlCommand command = new MySqlCommand("UPDATE `users` SET `" + column + "`=@value Where `userid`=@id ", connection);
                    string id = user_id_textbox.Text;
                    command.Parameters.AddWithValue("id", id);

                    //command.Parameters.AddWithValue("column", "`"+column+"`");
                    string value = user_textbox_new_value.Text;
                    command.Parameters.AddWithValue("value", value);
                    command.ExecuteNonQuery();
                    connection.Close();
                    users_label_status.Content = "Column " + column + " updated for user " + id + " with value ->" + value;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    users_label_status.Content = "Something went wrong";
                }
            }
            else if (action == "delete")
            {
                try
                {
                    MySqlConnection connection = new MySqlConnection(static_connectionString);
                    connection.Open();

                    MySqlCommand command = new MySqlCommand("DELETE from `users` Where `userid`=@id ", connection);
                    string id = user_id_textbox.Text;
                    command.Parameters.AddWithValue("id", id);

                    command.ExecuteNonQuery();
                    connection.Close();
                    users_label_status.Content = "User " + id + " Deleted Successfully";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    users_label_status.Content = "Something went wrong";
                }
            }
            else
            {
                users_label_status.Content = "Something went wrong";
            }

        }
        private void Ads_Action_Button_Click(object sender, RoutedEventArgs e)
        {
            string action = ads_action.Text;
            if (action == "select")
            {
                try
                {
                    MySqlConnection connection = new MySqlConnection(static_connectionString);
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("select * from ads where userid=@id", connection);
                    string id = ads_id_textbox.Text;
                    command.Parameters.AddWithValue("id", id);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string output = "";
                            output = " User ID:" + reader["userId"].ToString();
                            output += " Description: " + reader["description"].ToString();
                            output += " Category: " + reader["categoryId"].ToString();
                            output += " SuperAd: " + reader["superAd"].ToString();
                            output += " Address: " + reader["address"].ToString();
                            ads_label_status.Content = output;
                        }
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    ads_label_status.Content = "Something went wrong";
                }
            }
            else if (action == "update")
            {
                try
                {
                    MySqlConnection connection = new MySqlConnection(static_connectionString);
                    connection.Open();
                    string column = ads_table_column.Text;
                    MySqlCommand command = new MySqlCommand("UPDATE `ads` SET `" + column + "`=@value Where `adId`=@id ", connection);
                    string id = ads_id_textbox.Text;
                    command.Parameters.AddWithValue("id", id);

                    string value = ads_textbox_new_value.Text;
                    command.Parameters.AddWithValue("value", value);
                    command.ExecuteNonQuery();
                    connection.Close();
                    ads_label_status.Content = "Column " + column + " updated for Ad " + id + " with value ->" + value;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    ads_label_status.Content = "Something went wrong";
                }
            }
            else if (action == "delete")
            {
                try
                {
                    MySqlConnection connection = new MySqlConnection(static_connectionString);
                    connection.Open();

                    MySqlCommand command = new MySqlCommand("DELETE FROM propertyvalue WHERE adId=@id", connection);
                    string id = ads_id_textbox.Text;
                    command.Parameters.AddWithValue("id", id);
                    command.ExecuteNonQuery();

                    command = new MySqlCommand("DELETE FROM ads WHERE adId=@id", connection);
                    command.Parameters.AddWithValue("id", id);
                    command.ExecuteNonQuery();

                    connection.Close();
                    ads_label_status.Content = "Ad " + id + " Deleted Successfully";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    ads_label_status.Content = "Something went wrong";
                }
            }
            else
            {
                ads_label_status.Content = "Something went wrong";
            }
        }

        private void Alert_Restore_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(static_connectionString);
                connection.Open();
                MySqlCommand command = new MySqlCommand("UPDATE `ads` SET `ban_status`=@status Where `adid`=@adid  ", connection);
                string adid = alert_id_textbox.Text;
                command.Parameters.AddWithValue("adid", adid);

                string status = "ok";
                command.Parameters.AddWithValue("status", status);

                command.ExecuteNonQuery();
                connection.Close();
                alert_label_status.Content = "Ad Updated as : ok";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                alert_label_status.Content = "Something went wrong";
            }
        }

        private void Alert_Ban_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(static_connectionString);
                connection.Open();
                MySqlCommand command = new MySqlCommand("UPDATE `ads` SET `ban_status`=@status Where `adid`=@adid  ", connection);
                string adid = alert_id_textbox.Text;
                command.Parameters.AddWithValue("adid", adid);

                string status = "ban";
                command.Parameters.AddWithValue("status", status);

                command.ExecuteNonQuery();
                connection.Close();
                alert_label_status.Content = "Ad Updated as : ban";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                alert_label_status.Content = "Something went wrong";
            }
        }
    }
}

