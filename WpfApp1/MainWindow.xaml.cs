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
            InitializeComponent();
        }
        private void Logout_Button_Click(object sender, RoutedEventArgs e)
        {
            KonstantinosManeadis.Login_page.Logout();
            //Application.Current.Shutdown();
        }
        private void LogoutandExit_Button_Click(object sender, RoutedEventArgs e)
        {
            KonstantinosManeadis.Login_page.Logout();
            Application.Current.Shutdown();
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
            //dokimastika Main_Content_Frame.Content = new KonstantinosManeadis.UserProfile();
            Main_Content_Frame.Content = new realEstate_DimitrisAnastasiadis.Menu();
        }
        private void UserProfile_Button_Click(object sender, RoutedEventArgs e)
        {
            Main_Content_Frame.Content = new KonstantinosManeadis.UserProfile();
        }

        private void Administrator_Button_Click(object sender, RoutedEventArgs e)
        {
            Main_Content_Frame.Content = new KonstantinosManeadis.Administrator.Administrator_main();
        }

        private void Manager_Button_Click(object sender, RoutedEventArgs e)
        {
            Main_Content_Frame.Content = new KonstantinosManeadis.Manager.Manager_main();
        }
        private void About_Button_Click(object sender, RoutedEventArgs e)
        {
            KonstantinosManeadis.About_window newWindows = new KonstantinosManeadis.About_window();
            newWindows.Show();
            
        }
        
    }
}
