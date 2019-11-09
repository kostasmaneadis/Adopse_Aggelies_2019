using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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


namespace ChristosOuzouProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly string constring = "SERVER=localhost;DATABASE=mydb;UID=christos;PASSWORD=2341093066;";
        public MainWindow()
        {
            //InitializeComponent();
            /*string constring = "SERVER=localhost;DATABASE=mydb;UID=christos;PASSWORD=2341093066;";
            StringBuilder errorMessages = new StringBuilder();
            try
            {
                MySqlConnection conn = new MySqlConnection(constring);
                conn.Open();
                MySqlCommand comm1 = new MySqlCommand("INSERT INTO users VALUES('USRNAM','PASS','SURNAM','FATHER','1900-1-1','ROLE')", conn);
                comm1.ExecuteNonQuery();
                MySqlCommand comm = new MySqlCommand("SELECT * FROM users", conn);

                DataTable dt = new DataTable();
                dt.Load(comm.ExecuteReader());
                dtgrid.DataContext = dt;
                conn.Close();
            }
            catch (MySqlException ex)
            {
                
                System.Diagnostics.Debug.WriteLine(errorMessages.ToString());
            }*/
           
        }
        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            try
            {
                MySqlConnection conn = new MySqlConnection(constring);
                conn.Open();
                MySqlCommand comm = new MySqlCommand("SELECT DISTINCT name FROM subcategories WHERE catId=1", conn);
                MySqlDataReader rdr = comm.ExecuteReader();
                while (rdr.Read())
                    data.Add(rdr["name"].ToString());
                rdr.Close();
                var comboBox = sender as ComboBox;
                comboBox.ItemsSource = data;

            }
            catch (MySqlException ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
    }

}
    

