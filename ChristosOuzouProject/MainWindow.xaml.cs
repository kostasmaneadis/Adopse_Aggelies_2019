using Microsoft.Win32;
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
        List<BitmapImage> imgList = new List<BitmapImage>();
;        public MainWindow()
        {
            InitializeComponent();
            CB_fill();

        }
        private void CB_fill()
        {
            List<string> data = new List<string>();
            List<string> data1 = new List<string>();
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
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand("SELECT DISTINCT region FROM addresses", conn);
                MySqlDataReader rdr = comm.ExecuteReader();
                while (rdr.Read())
                    data1.Add(rdr["region"].ToString());
                rdr.Close();
                conn.Close();
                regionCB.ItemsSource = data1;
            }
            catch (MySqlException ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
        public void Subcat_fill(object sender, EventArgs e)
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
                conn.Close();
            }
            catch (MySqlException ex){
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            
        }
        public void City_fill(object sender, EventArgs e)
        {
            try
            {
                List<string> data = new List<string>();
                conn.Open();
                MySqlCommand comm = new MySqlCommand("SELECT DISTINCT municipality FROM addresses WHERE region LIKE @par1", conn);
                comm.Parameters.AddWithValue("@par1", regionCB.Text);
                MySqlDataReader rdr = comm.ExecuteReader();
                while (rdr.Read())
                    data.Add(rdr["municipality"].ToString());
                rdr.Close();
                cityCB.ItemsSource = data;
                conn.Close();
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

        }
        public void Address_fill(object sender, EventArgs e)
        {
            try
            {
                List<string> data = new List<string>();
                conn.Open();
                MySqlCommand comm = new MySqlCommand("SELECT address FROM addresses WHERE (region LIKE @par1 AND municipality LIKE @par2)", conn);
                comm.Parameters.AddWithValue("@par1", regionCB.Text);
                comm.Parameters.AddWithValue("@par2", cityCB.Text);
                MySqlDataReader rdr = comm.ExecuteReader();
                while (rdr.Read())
                    data.Add(rdr["address"].ToString());
                rdr.Close();
                addressCB.ItemsSource = data;
                conn.Close();
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

        }
        public void UploadImg(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Πρόσθεσε φωτογραφίες";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            op.Multiselect = true;
            if (op.ShowDialog() == true)
            {
                foreach (string filename in op.FileNames)
                    imgList.Add(new BitmapImage(new Uri(filename)));
            }

        }

    }

}
    

