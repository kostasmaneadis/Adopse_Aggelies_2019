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
    /// Interaction logic for ViewJob.xaml
    /// </summary>
    public partial class ViewJob : Page
    {
        static string constring = "SERVER=localhost;DATABASE=mydb;UID=christos;PASSWORD=2341093066;";
        MySqlConnection conn = new MySqlConnection(constring);
        List<BitmapImage> imgList = new List<BitmapImage>();
        /* MySqlCommand comm = new MySqlCommand("SELECT region, municipality, address FROM addresses AD WHERE AD.id IN (SELECT addressid FROM fulladdress WHERE id IN (SELECT address FROM ads WHERE ))",conn);
        MySqlDataReader rdr = comm.ExecuteReader();*/
        public ViewJob()
        {
            InitializeComponent();
        }
        private void fillad(MySqlDataReader rdr)
        {
            if (rdr.Read())
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand("SELECT * FROM photos WHERE adsId IN @id",conn);
                comm.Parameters.AddWithValue("@id", rdr["id"].ToString());
                MySqlDataReader rdr2 = comm.ExecuteReader();
                while(rdr2.Read())
                    imgList.Add(new BitmapImage(new Uri(rdr2["photopath"].ToString())));
                foreach (BitmapImage i in imgList)
                {
                    Image img = new Image();
                    img.Source = i;
                    fullimagespanel.Children.Add(img);
                }
                jobLocationL.Content = rdr["region"].ToString() + ", " + rdr["municipality"].ToString();
                 jobAddressL.Content= rdr["address"].ToString() + ", " + rdr["number"].ToString();
                jobKindL.Content= rdr["title"].ToString();
                jobSalaryL.Content = rdr["salary"].ToString();
                descriptionTB.Text = rdr["description"].ToString();
                jobadprevImg.Source = imgList.First();
            }
        }
    }
}
