using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
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
        int imagecounter = -1;
        public MainWindow()
        {
            InitializeComponent();
            CB_fill();
            ViewJob vj = new ViewJob();
            vj.Show();
        }
        private void CB_fill()
        {
            List<string> data = new List<string>();
            List<string> data1 = new List<string>();
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand("SELECT title FROM category WHERE parentId=1", conn);
                MySqlDataReader rdr = comm.ExecuteReader();
                while (rdr.Read())
                    data.Add(rdr["title"].ToString());
                rdr.Close();
                mainCatCB.ItemsSource = data;
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
            try
            {
                List<string> data = new List<string>();
                conn.Open();
                MySqlCommand comm = new MySqlCommand("SELECT title FROM category WHERE parentId = (SELECT categoryId FROM category WHERE title LIKE @par1)", conn);
                comm.Parameters.AddWithValue("@par1", mainCatCB.Text);
                MySqlDataReader rdr = comm.ExecuteReader();
                while (rdr.Read())
                    data.Add(rdr["title"].ToString());
                rdr.Close();
                subCatCB.ItemsSource = data;
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
            uploadImg.Source = imgList.First();
            uploadImg.Visibility = Visibility.Visible;
            rmB.Visibility = Visibility.Visible;
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



        private void CreateJobAd(object sender, RoutedEventArgs e)
        {
            try
            {
                int adid = 0;
                conn.Open();
                MySqlCommand comm = new MySqlCommand("SELECT id FROM addresses WHERE(address LIKE @address AND municipality LIKE @mun AND region LIKE @reg)", conn);
                comm.Parameters.AddWithValue("@address", addressCB.SelectedValue.ToString());
                comm.Parameters.AddWithValue("@mun", cityCB.SelectedValue.ToString());
                comm.Parameters.AddWithValue("@reg", regionCB.SelectedValue.ToString());
                MySqlDataReader rdr = comm.ExecuteReader();
                if (rdr.Read())
                    adid = Convert.ToInt32(rdr["id"]);
                rdr.Close();
                comm = new MySqlCommand("INSERT INTO fulladdress(addressid, number) VALUES( @adid,@number)", conn);
                comm.Parameters.AddWithValue("@adid", adid);
                comm.Parameters.AddWithValue("@number", addressNumberTB.Text);
                comm.ExecuteNonQuery();
                long adrId = comm.LastInsertedId;
                comm = new MySqlCommand("" +
                    "INSERT INTO Ads(userId, dateAdded,description,superAd,address) VALUES" +
                    "( @userId, @date, @descr,false,@address)", conn);
                comm.Parameters.AddWithValue("@userId", 1);
                comm.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                comm.Parameters.AddWithValue("@descr", DescriptionTB.Text);
                comm.Parameters.AddWithValue("@address", adrId);
                comm.ExecuteNonQuery();
                long adId = comm.LastInsertedId;
                comm = new MySqlCommand("INSERT INTO propertyvalue VALUES(@adid,1,@fptime)", conn);
                comm.Parameters.AddWithValue("@adid", adId);
                ComboBoxItem cmbItem = foul_part_timeCB.SelectedItem as ComboBoxItem;
                comm.Parameters.AddWithValue("@fptime", cmbItem.Content.ToString());
                comm.ExecuteNonQuery();
                comm = new MySqlCommand("INSERT INTO propertyvalue VALUES(@adid,2,@salary)", conn);
                comm.Parameters.AddWithValue("@adid", adId);
                comm.Parameters.AddWithValue("@salary", salaryTB.Text);
                comm.ExecuteNonQuery();
                if (imgList.Count > 0)
                {
                    foreach (BitmapImage i in imgList)
                    {
                        comm = new MySqlCommand("INSERT INTO photos(adsId,photo) VALUES(@adid,@photo)", conn);
                        byte[] data = null;
                        /* JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                         System.Diagnostics.Debug.WriteLine("ok1");
                         encoder.Frames.Add(BitmapFrame.Create(i);
                         System.Diagnostics.Debug.WriteLine("ok2");
                         using (MemoryStream ms = new MemoryStream())
                         {
                             System.Diagnostics.Debug.WriteLine("ok3");
                             encoder.Save(ms);
                             System.Diagnostics.Debug.WriteLine("ok4");
                             data = ms.ToArray();
                             System.Diagnostics.Debug.WriteLine("ok");
                         }*/
                        System.Diagnostics.Debug.WriteLine(i.ToString());
                        var ms = new MemoryStream();
                        var jpgEncoder = new JpegBitmapEncoder();
                        jpgEncoder.Frames.Add(BitmapFrame.Create(i));
                        jpgEncoder.Save(ms);
                        data = ms.GetBuffer();
                        comm.Parameters.AddWithValue("@adid", adId);
                        comm.Parameters.AddWithValue("@photo", data);
                        comm.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            
        }
    }
}

    

