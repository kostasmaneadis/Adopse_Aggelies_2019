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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void newAdButton_Click(object sender, RoutedEventArgs e)
        {
            newAd newAdpage = new newAd();
            NavigationService.Navigate(newAdpage);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            showAd showAdPage = new showAd();
            NavigationService.Navigate(showAdPage);
        }
    }
}
