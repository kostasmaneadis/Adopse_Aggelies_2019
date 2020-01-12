﻿using System;
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
            AddJob newAdpage = new AddJob();
            NavigationService.Navigate(newAdpage);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewJob showAdsPanelPage = new ViewJob();
            NavigationService.Navigate(showAdsPanelPage);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
             
        }
    }
}
