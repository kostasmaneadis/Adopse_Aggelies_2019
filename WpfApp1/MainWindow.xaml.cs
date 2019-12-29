using System;
using System.Collections.Generic;
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

using System.Data.SqlClient;

using System.Data;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //InitializeComponent();
        }
        private void ButtomLogout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            //ButtonOpenMenu.Visibility = Visibility.Collapsed;
            //ButtonCloseMenu.Visibility = Visibility.Visible;

        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            //ButtonOpenMenu.Visibility = Visibility.Visible;
            //ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            Main_Content_Frame.Content = new KonstantinosManeadis.Login_page();
        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            Main_Content_Frame.Content = new KonstantinosManeadis.Register_page();
        }

        private void Jobs_Button_Click(object sender, RoutedEventArgs e)
        {
            //dokimastika
            Main_Content_Frame.Content = new ChristosOuzouProject.MainWindow();
        }

        private void Apartments_Button_Click(object sender, RoutedEventArgs e)
        {
            //dokimastika
            Main_Content_Frame.Content = new realEstate_DimitrisAnastasiadis.Menu();
        }
    }
}
