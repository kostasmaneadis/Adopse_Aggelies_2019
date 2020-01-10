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
    /// Interaction logic for deleteAd.xaml
    /// </summary>
    public partial class deleteAd : Page
    {
        public deleteAd()
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
            String currentUserId = KonstantinosManeadis.Login_page.GetUserID();
            List<AdtoDisplay> AdstoDisplay = new List<AdtoDisplay>();
            List<String> AdIds = new List<String>();
            AdIds = database.selectQuery($"SELECT a.adId FROM ads a WHERE a.userId={currentUserId}");

            if (AdIds.Count == 0)
            {
                noAdsMessage.Visibility = Visibility.Visible;
                AdsList.Visibility = Visibility.Collapsed;
            }
                

            foreach (String item in AdIds)
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

                AdstoDisplay.Add(new AdtoDisplay() { adId = item, DisplayText = Text });
            }

            AdsList.DisplayMemberPath = "DisplayText";
            AdsList.ItemsSource = AdstoDisplay;
        }

        private void AdsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            String selectedAdId = (AdsList.SelectedItem as AdtoDisplay).adId;
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show($"Είστε σίγουροι πως θέλετε να διαγράψετε την αγγελία {(AdsList.SelectedItem as AdtoDisplay).DisplayText}?",
                "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                database.insertQuery($"DELETE FROM propertyvalue WHERE adId={selectedAdId}");
                database.insertQuery($"DELETE FROM ads WHERE adId={selectedAdId}");
                fillListBox();
            }
        }
    }
}
