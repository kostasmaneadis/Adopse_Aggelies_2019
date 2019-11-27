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
    /// Interaction logic for newAd.xaml
    /// </summary>
    public partial class newAd : Page
    {
        public newAd()
        {
            InitializeComponent();
            nomosCB.ItemsSource = database.selectQuery("SELECT DISTINCT region FROM addresses");
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            String kind = "", sell_rent ="", status = "", dateBuilt, description;
            float area, price;
            int bedrooms, bathrooms;
            List<int> floor = new List<int>();

            if (diamerismaRB.IsChecked == true)
                kind = "Διαμέρισμα";
            else if (ktirioRB.IsChecked == true)
                kind = "Κτήριο";
            else if (mezonetaRB.IsChecked == true)
                kind = "Μεζονέτα";
            else if (monokatoikiaRB.IsChecked == true)
                kind = "Μονοκατοικία";

            if (neodmitoRB.IsChecked == true)
                status = "Νεόδμητο";
            else if (imitelesRB.IsChecked == true)
                status = "Ημιτελές";
            else if (ipoKataskeviRB.IsChecked == true)
                status = "Υπό Κατασκευή";
            else if (alloRB.IsChecked == true)
                status = "Άλλο";

            if (ipogioCHB.IsChecked == true) floor.Add(-1);
            if (isogioCHB.IsChecked == true) floor.Add(0);
            if (floor1CHB.IsChecked == true) floor.Add(1);
            if (floor2CHB.IsChecked == true) floor.Add(2);
            if (floor3CHB.IsChecked == true) floor.Add(3);
            if (floor4CHB.IsChecked == true) floor.Add(4);
            if (floor5CHB.IsChecked == true) floor.Add(5);
            if (floor6CHB.IsChecked == true) floor.Add(6);
            if (floor7CHB.IsChecked == true) floor.Add(7);
            if (floor8CHB.IsChecked == true) floor.Add(8);

            area = float.Parse(areaTB.Text);
            dateBuilt = dateBuiltTB.Text;
            bedrooms = int.Parse(bedroomsTB.Text);
            bathrooms = int.Parse(bathroomsTB.Text);
            price = float.Parse(priceTB.Text);

            if (polisiRB.IsChecked == true)
                sell_rent = "Πώληση";
            else if (enikiasiRB.IsChecked == true)
                sell_rent = "Ενοικίαση";

            description = descriptionTB.Text;

            List<String> addressIDString;
            addressIDString = database.selectQuery("SELECT id from addresses where region ='" + nomosCB.Text + "' and municipality ='" + dimosCB.Text + "' and address ='" + odosCB.Text + "'");
            int addressID = int.Parse(addressIDString[0]);
            long fullAddressID = database.insertAutoIncrement("insert into fulladdress(addressid,number) values(" + addressID + ", " + arithmosTB.Text +")");

            long adID = database.insertAutoIncrement("INSERT INTO ads(dateAdded,description,categoryId,superAd,address) VALUES(now(), '" + description + "', 2, 0, " + fullAddressID + ")");
            testBlock.Text = adID.ToString();

            database.insertQuery("INSERT INTO propertyvalue(adId, propertyId, stringValue) VALUES(" + adID + ", 1, '" + sell_rent + "')");
            database.insertQuery("INSERT INTO propertyvalue(adId, propertyId, stringValue) VALUES(" + adID + ", 2, '" + price + "')");
            database.insertQuery("INSERT INTO propertyvalue(adId, propertyId, stringValue) VALUES(" + adID + ", 3, '" + area + "')");
            database.insertQuery("INSERT INTO propertyvalue(adId, propertyId, stringValue) VALUES(" + adID + ", 4, '" + kind + "')");
            database.insertQuery("INSERT INTO propertyvalue(adId, propertyId, stringValue) VALUES(" + adID + ", 6, '" + status + "')");
            database.insertQuery("INSERT INTO propertyvalue(adId, propertyId, stringValue) VALUES(" + adID + ", 7, '" + dateBuilt + "')");
            database.insertQuery("INSERT INTO propertyvalue(adId, propertyId, stringValue) VALUES(" + adID + ", 8, '" + bedrooms + "')");
            database.insertQuery("INSERT INTO propertyvalue(adId, propertyId, stringValue) VALUES(" + adID + ", 9, '" + bathrooms + "')");
            foreach (int item in floor)
                database.insertQuery("INSERT INTO propertyvalue(adId, propertyId, stringValue) VALUES(" + adID + ", 5, '" + item + "')");

        }

        private void nomosCB_DropDownClosed(object sender, EventArgs e)
        {
            dimosCB.ItemsSource = database.selectQuery("SELECT DISTINCT municipality FROM addresses WHERE region ='" + nomosCB.Text + "'");
            odosCB.ItemsSource = null;
        }

        private void dimosCB_DropDownClosed(object sender, EventArgs e)
        {
            odosCB.ItemsSource = database.selectQuery("SELECT DISTINCT address FROM addresses WHERE municipality ='" + dimosCB.Text + "'");
        }
    }
}
