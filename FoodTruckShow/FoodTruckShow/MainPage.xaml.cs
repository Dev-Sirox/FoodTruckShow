using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using FoodTruckShow.Resources;
using Parse;
using FoodTruckShow.Assets;
namespace FoodTruckShow
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void btnEntrar_Click(object sender, RoutedEventArgs e)
        {
            if((txt_email.Text != "")&&(txt_senha.Password != ""))
            {
                verificar_user();
            }
            else
            {
                MessageBoxResult resultado = MessageBox.Show("E nescessario preencher todos os campos.", "Atenção!", MessageBoxButton.OK);
            }
                 
        }

        private async void verificar_user()
        {
            String id = "";
            bool soufoodtruck = false;
            
            ParseQuery<ParseObject> query = ParseObject.GetQuery("Usuarios").WhereEqualTo("Email", txt_email.Text).WhereEqualTo("Senha", txt_senha.Password);
            IEnumerable<ParseObject> resultado = await query.FindAsync();
           
            foreach (ParseObject usuario in resultado)
            {
                id = usuario.ObjectId;
                soufoodtruck = usuario.Get<bool>("foodtruck");
            }  

            if(soufoodtruck == true)
            {
                MessageBoxResult primeira_visita = MessageBox.Show( "Id numero = " + id , "Seja Bem-Vindo!", MessageBoxButton.OK);
                NavigationService.Navigate(new Uri("/SouFoodTruck.xaml?userId=" + id, UriKind.Relative));
            }
            else
            {
                NavigationService.Navigate(new Uri("/SouCliente.xaml?userId=" + id, UriKind.Relative));
            }
             
        }


        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}