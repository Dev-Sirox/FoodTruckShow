using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using Microsoft.Phone.Maps.Services;
using System.Text;

namespace FoodTruck
{
    public partial class PageFoodTruck : PhoneApplicationPage
    {
        public RouteQuery routeQuery = new RouteQuery();
        public StringBuilder sb = new StringBuilder();

        public PageFoodTruck()
        {
            InitializeComponent();
            MostraMapa();
        }

        private void MostraMapa()
        {
            this.mapaLocal.Layers.Add(new MapLayer()
            {
                new MapOverlay()
                {
                    GeoCoordinate = new System.Device.Location.GeoCoordinate(-23.6048658, -46.4488104),
                    Content = new Ellipse()
                    {
                        Fill = new SolidColorBrush(Colors.Red),
                        Width = 25,
                        Height = 25
                    }
                }
            });
        }

        private void btnZoonIn_Click(object sender, RoutedEventArgs e)
        {
            this.mapaLocal.ZoomLevel = Math.Min(20, this.mapaLocal.ZoomLevel + 1);
        }

        private void btnZoonOut_Click(object sender, RoutedEventArgs e)
        {
            this.mapaLocal.ZoomLevel = Math.Max(1, this.mapaLocal.ZoomLevel - 1);
        }

        //Visões do Mapa
        private void btnPadrão_Click(object sender, EventArgs e)
        {
            this.mapaLocal.CartographicMode = Microsoft.Phone.Maps.Controls.MapCartographicMode.Road;
        }

        private void btnAerial_Click(object sender, EventArgs e)
        {
            this.mapaLocal.CartographicMode = Microsoft.Phone.Maps.Controls.MapCartographicMode.Aerial;
        }
        //FIM_Visões do Mapa

    }
}