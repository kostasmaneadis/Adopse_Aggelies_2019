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

namespace ChristosOuzouProject
{
    /// <summary>
    /// Interaction logic for Menu_cars.xaml
    /// </summary>
    public partial class Menu_cars : Page
    {
        public Menu_cars()
        {
            InitializeComponent();
        }
        private void newAdButton_Click(object sender, RoutedEventArgs e)
        {
            AddCar newAdpage = new AddCar();
            NavigationService.Navigate(newAdpage);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewCar showAdsPanelPage = new ViewCar();
            NavigationService.Navigate(showAdsPanelPage);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
