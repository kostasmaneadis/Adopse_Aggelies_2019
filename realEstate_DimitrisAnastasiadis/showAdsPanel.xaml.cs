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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace realEstate_DimitrisAnastasiadis
{
    /// <summary>
    /// Interaction logic for showAdsPanel.xaml
    /// </summary>
    public partial class showAdsPanel : Page
    {
        public static String selectedAdId;

        public showAdsPanel()
        {
            InitializeComponent();
            fillListBox();
        }

        private class AdtoDisplay
        {
            public String adId { get; set; }
            public String DisplayText { get; set; }
        }

        private void fillListBox()
        {
            List<AdtoDisplay> AdstoDisplay = new List<AdtoDisplay>();
            List<String> AdIds = new List<String>();
            AdIds = database.selectQuery("SELECT a.adId FROM ads a");

            foreach(String item in AdIds)
            {
                List<String> selectReturn = new List<String>();
                String Text = "";
                selectReturn = database.selectQuery($"SELECT a.stringValue FROM propertyvalue a WHERE a.adId={item} and a.propertyId=4");
                Text += selectReturn[0] + " ";
                selectReturn = database.selectQuery($"SELECT b.municipality FROM ads a JOIN fulladdress fa ON a.address=fa.id JOIN addresses b on fa.addressid=b.id WHERE a.adId={item}");
                Text += selectReturn[0] + " - ";
                selectReturn = database.selectQuery($"SELECT b.region FROM ads a JOIN fulladdress fa ON a.address=fa.id JOIN addresses b on fa.addressid=b.id WHERE a.adId={item}");
                Text += selectReturn[0] + " ";
                selectReturn = database.selectQuery($"SELECT a.stringValue FROM propertyvalue a WHERE a.adId={item} and a.propertyId=2");
                float price = float.Parse(selectReturn[0]);
                Text += price.ToString("C0");

                AdstoDisplay.Add(new AdtoDisplay() { adId = item, DisplayText=Text });
            }

            AdsList.DisplayMemberPath = "DisplayText";
            AdsList.ItemsSource = AdstoDisplay;
        }

        private void AdsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            selectedAdId = (AdsList.SelectedItem as AdtoDisplay).adId;
            showAd showAdPage = new showAd();
            NavigationService.Navigate(showAdPage);
        }
    }
}
