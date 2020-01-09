using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChristosOuzouProject
{
    /// <summary>
    /// Interaction logic for AddCar.xaml
    /// </summary>
    public partial class AddCar : Window
    {
        static string constring = "SERVER=localhost;DATABASE=mydb;UID=christos;PASSWORD=2341093066;";
        MySqlConnection conn = new MySqlConnection(constring);
        int imagecounter = -1;
        List<BitmapImage> imgList = new List<BitmapImage>();
        List<string> data1 = new List<string>();
        List<string> data = new List<string>();
        List<string> data2 = new List<string>();
        public AddCar()
        {
            InitializeComponent();
            CbFill();
        }
        private void CbFill()
        {
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
                conn.Open();
                comm = new MySqlCommand("SELECT DISTINCT make FROM vehiclemodelyear", conn);
                rdr = comm.ExecuteReader();
                while (rdr.Read())
                    data.Add(rdr["make"].ToString());
                rdr.Close();
                conn.Close();
                makeCB.ItemsSource = data;
                conn.Open();
                comm = new MySqlCommand("SELECT  color FROM carcolors", conn);
                rdr = comm.ExecuteReader();
                while (rdr.Read())
                    data2.Add(rdr["color"].ToString());
                rdr.Close();
                conn.Close();
                colorCB.ItemsSource = data2;

            }
            catch (MySqlException ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        public void Model_fill(object sender, EventArgs e)
        {
            try
            {
                List<string> data = new List<string>();
                conn.Open();
                MySqlCommand comm = new MySqlCommand("SELECT model FROM vehiclemodelyear WHERE (make LIKE @par1)", conn);
                comm.Parameters.AddWithValue("@par1", makeCB.Text);
                MySqlDataReader rdr = comm.ExecuteReader();
                while (rdr.Read())
                    data.Add(rdr["model"].ToString());
                rdr.Close();
                modelCB.ItemsSource = data;
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
            uploadImg.Source = imgList.First();
            uploadImg.Visibility = Visibility.Visible;
            rmB.Visibility = Visibility.Visible;
            nextBT.Visibility = Visibility.Visible;
            beforeBT.Visibility = Visibility.Visible;
        }

        private void nextPhoto(object sender, RoutedEventArgs e)
        {
            Button bt = e.Source as Button;
            if (bt.Name == "nextBT")
                if (imagecounter >= (imgList.Count - 1))
                    imagecounter = 0;
                else
                    imagecounter++;
            else if (bt.Name == "beforeBT")
                if (imagecounter <= 0)
                    imagecounter = imgList.Count - 1;
                else
                    imagecounter--;
            uploadImg.Source = imgList.ElementAt(imagecounter);

        }
        private void RemoveImages(object sender, RoutedEventArgs e)
        {
            imgList.Clear();
            uploadImg.Visibility = Visibility.Hidden;
        }
    }
}