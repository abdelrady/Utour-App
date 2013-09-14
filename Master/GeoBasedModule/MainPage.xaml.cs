using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace GeoBasedModule
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            
           
        }

        //Augmented reality mode activated
        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/AugmentedRealityPage.xaml",UriKind.Relative));
        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/MapView.xaml", UriKind.Relative));
        }
    }
}