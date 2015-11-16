using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Parse;

namespace FoodTruckShow
{
    public partial class SouFoodTruck : PhoneApplicationPage
    {
        string food_id;
        string Id = "-1";
        

        public SouFoodTruck()
        {
            InitializeComponent();
        }



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (NavigationContext.QueryString.TryGetValue("userId", out Id))
            {
                if (!string.IsNullOrWhiteSpace(Id))
                {
                    buscar_foodtruck();  
                }else
                {
                    MessageBoxResult primeira_visita = MessageBox.Show("Erro " + Id + " sem numero" , "Erro!", MessageBoxButton.OK);
                }
               
            } 
        }

        private void btnmodificar_Click(object sender, EventArgs e)
        {

        }

        private void btAtualizar_Click(object sender, EventArgs e)
        {

        }

        private void btadd_Click(object sender, EventArgs e)
        {

        }

        private void btdel_Click(object sender, EventArgs e)
        {

        }

        private async void buscar_foodtruck()
        {
            ParseQuery<ParseObject> query = ParseObject.GetQuery("Foodtruck").WhereEqualTo("idUser", Id);
            IEnumerable<ParseObject> resultado = await query.FindAsync();

            foreach (ParseObject usuario in resultado)
            {
                food_id = usuario.ObjectId;
            }
            
            if(food_id == null)
            {
                MessageBoxResult primeira_visita = MessageBox.Show("Esse e seu primeiro acesso, precisamos saber mais sobre seu FoodTruck.", "Seja Bem-Vindo!", MessageBoxButton.OK);
                NavigationService.Navigate(new Uri("/CadastroFoodTruck.xaml?userId=" + Id, UriKind.Relative));
            }

        }
    }
}