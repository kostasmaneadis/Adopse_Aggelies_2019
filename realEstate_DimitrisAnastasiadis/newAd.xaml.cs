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

        }
    }
}
