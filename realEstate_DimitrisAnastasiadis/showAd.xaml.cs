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
    /// Interaction logic for showAd.xaml
    /// </summary>
    public partial class showAd : Page
    {

        long adID;
        public showAd()
        {
            InitializeComponent();
            fillPage();
        }

        private void fillPage()
        {
            List<String> selectReturn;
            float price, area;
            adID = long.Parse(showAdsPanel.selectedAdId);

            selectReturn = database.selectQuery($"SELECT a.stringValue FROM propertyvalue a WHERE a.adId={adID} and a.propertyId=4");
            kind.Text = selectReturn[0];
            selectReturn = database.selectQuery($"SELECT b.municipality FROM ads a JOIN fulladdress fa ON a.address=fa.id JOIN addresses b on fa.addressid=b.id WHERE a.adId={adID}");
            location.Text = selectReturn[0] + " - ";
            selectReturn = database.selectQuery($"SELECT b.region FROM ads a JOIN fulladdress fa ON a.address=fa.id JOIN addresses b on fa.addressid=b.id WHERE a.adId={adID}");
            location.Text += selectReturn[0];
            selectReturn = database.selectQuery($"SELECT a.stringValue FROM propertyvalue a WHERE a.adId={adID} and a.propertyId=2");
            price = float.Parse(selectReturn[0]);
            priceTB.Text = price.ToString("C0");
            selectReturn = database.selectQuery($"SELECT a.stringValue FROM propertyvalue a WHERE a.adId={adID} and a.propertyId=3");
            areaTB.Text = selectReturn[0] + " τ.μ.";
            area = float.Parse(selectReturn[0]);
            costPerTM.Text = (price / area).ToString("C0") + " / τ.μ";
            selectReturn = database.selectQuery($"SELECT a.stringValue FROM propertyvalue a WHERE a.adId={adID} and a.propertyId=8");
            bedrooms.Text = selectReturn[0];
            selectReturn = database.selectQuery($"SELECT a.stringValue FROM propertyvalue a WHERE a.adId={adID} and a.propertyId=9");
            bathrooms.Text = selectReturn[0];

            //selectReturn = database.selectQuery($"SELECT date_format(stringValue,  '%d/%m/%Y') FROM propertyvalue a WHERE a.propertyId=7 and a.adId={adID}");
            //dateBuiltTB.Text += selectReturn[0];
            selectReturn = database.selectQuery($"SELECT a.stringValue FROM propertyvalue a WHERE a.adId={adID} and a.propertyId=6");
            statusTB.Text += selectReturn[0];
            selectReturn = database.selectQuery($"SELECT a.stringValue FROM propertyvalue a WHERE a.adId={adID} and a.propertyId=5");
            foreach (String item in selectReturn)
                floorsTB.Text += item + ",";
            floorsTB.Text = (floorsTB.Text).Trim(',');


            selectReturn = database.selectQuery($"SELECT a.description FROM ads a WHERE a.adId={adID}");
            descriptionTB.Text = selectReturn[0];
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            database.insertQuery($"UPDATE ads SET ban_status='alert' where adId={adID}");
        }
    }
}
