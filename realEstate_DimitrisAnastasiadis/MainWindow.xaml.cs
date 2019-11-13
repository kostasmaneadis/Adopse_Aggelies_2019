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
using MySql.Data.MySqlClient;
using System.Data;

namespace realEstate_DimitrisAnastasiadis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection con = new MySqlConnection("server=localhost;user id=root;password=;database=aggeliestest");
        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new Menu();
        }

        public void dbInsert()
        {

            con.Open();
            MySqlCommand command = new MySqlCommand("delete from users where userId=2", con);
            command.ExecuteNonQuery();
            con.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dbInsert();
        }
    }
}
