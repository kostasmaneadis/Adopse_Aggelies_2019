﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace KonstantinosManeadis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(sender.ToString(), "username: " + username_textbox.Text);
            System.Diagnostics.Debug.WriteLine(sender.ToString(), "password: " + password_textbox.Password);
            try
            {
                SqlConnection cn_connection = DB_Connection;
                SqlCommand cmd_Command = new SqlCommand("insert into user_table(username, password) values(@username , @password)" , cn_connection);
                string username = username_textbox.Text;
                cmd_Command.Parameters.AddWithValue("username", username);
                string password = password_textbox.Password;
                cmd_Command.Parameters.AddWithValue("password", password);

                cmd_Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static SqlConnection DB_Connection
        {
            get
            {
                string cn_String = Properties.Settings1.Default.connection_String;
                SqlConnection cn_connection = new SqlConnection(cn_String);
                if (cn_connection.State != ConnectionState.Open) cn_connection.Open();
                return cn_connection;
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            
            
            
        }
    }
}