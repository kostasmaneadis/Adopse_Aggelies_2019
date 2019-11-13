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
        static string constring = "SERVER=localhost;DATABASE=mydb;UID=christos;PASSWORD=2341093066;";
        MySqlConnection conn = new MySqlConnection(constring);
        public MainWindow()
        {
            InitializeComponent();
            CB_fill();

        }
        private void CB_fill()
        {
            List<string> data = new List<string>();
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand("SELECT DISTINCT name FROM subcategories WHERE catId=1", conn);
                MySqlDataReader rdr = comm.ExecuteReader();
                while (rdr.Read())
                    data.Add(rdr["name"].ToString());
                rdr.Close();
                mainCatCB.ItemsSource=data;
                conn.Close();
            }
            catch (MySqlException ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
        public void subcat_fill(object sender, EventArgs e)
        {
            try {
                List<string> data = new List<string>();
                conn.Open();
                MySqlCommand comm = new MySqlCommand("SELECT sub2 FROM subcategories WHERE name LIKE @par1",conn);
                comm.Parameters.AddWithValue("@par1", mainCatCB.Text);
                MySqlDataReader rdr = comm.ExecuteReader();
                while (rdr.Read())
                    data.Add(rdr["sub2"].ToString());
                rdr.Close();
                subCatCB.ItemsSource = data;
                data.Add(mainCatCB.Text);
                conn.Close();
            }
            catch (MySqlException ex){
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            
        }
        
    }

}
    

