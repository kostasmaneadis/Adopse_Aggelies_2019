using System;
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

namespace WpfApp1
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
        public static SqlConnection Get_DB_Connection()

        {

            string cn_String = Properties.Settings1.Default.connection_String;

            SqlConnection cn_connection = new SqlConnection(cn_String);

            if (cn_connection.State != ConnectionState.Open) cn_connection.Open();


            return cn_connection;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine( sender.ToString(), "Button: ");
        }
        private void Insert_User(object sender, RoutedEventArgs e)
        {
           
                try
                {
                SqlConnection cn_connection = Get_DB_Connection();


                

                SqlCommand cmd_Command = new SqlCommand("insert into user_table(username, password) values('kostas','123')", cn_connection);

                cmd_Command.ExecuteNonQuery();
                
            }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            
        }
    }
}
