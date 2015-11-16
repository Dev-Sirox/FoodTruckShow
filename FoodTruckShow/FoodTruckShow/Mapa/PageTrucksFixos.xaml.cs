using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace FoodTruck
{
    public partial class TrucksFicosPage : PhoneApplicationPage
    {
        public string uf;
        public string cidade;

        public TrucksFicosPage()
        {
            InitializeComponent();
        }

        private void mainLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NavigationService.Navigate(new Uri("PageFoodTruckFixoEstado.xaml", UriKind.Relative));
        }

    }
}