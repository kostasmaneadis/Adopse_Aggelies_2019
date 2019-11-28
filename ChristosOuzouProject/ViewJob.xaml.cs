using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
    public partial class ViewJob : Window
    {
        public ViewJob()
        {
            InitializeComponent();
           
        }
        private void fillad(MySqlDataReader rdr)
        {
            if (rdr.Read())
            {
               // jobTitleL.Content = 
                jobLocationL.Content = rdr["region"].ToString() + ", " + rdr["municipality"].ToString();
                 jobAddressL.Content= rdr["address"].ToString() + ", " + rdr["number"].ToString();
                jobKindL.Content= rdr["title"].ToString();
                jobSalaryL.Content = rdr["salary"].ToString();
                descriptionTB.Text = rdr["description"].ToString();
            }
        }
    }
}
