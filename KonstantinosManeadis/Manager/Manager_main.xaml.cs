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

namespace KonstantinosManeadis.Manager
{
    /// <summary>
    /// Interaction logic for Manager_main.xaml
    /// </summary>
    public partial class Manager_main : Page
    {
        public Manager_main()
        {
            InitializeComponent();
            string LogedInUserRole = KonstantinosManeadis.Login_page.GetUserRole();
            if (LogedInUserRole != "administrator" )
            {
                if (LogedInUserRole != "manager")
                {
                    main_tab_control.Visibility = Visibility.Hidden;
                    no_access_label.Content = "You must be Administrator or Manager to access this page..";
                }
            }
        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
