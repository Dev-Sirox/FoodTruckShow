using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Device.Location;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Maps.Services;
using Microsoft.Phone.Maps.Controls;
using Parse;
using System.Text;

namespace FoodTruckShow
{
    public partial class CadastroFoodTruck : PhoneApplicationPage
    {
        string Id;        
        //public StringBuilder sb = new StringBuilder();
        public CadastroFoodTruck()
        {
            InitializeComponent();
           
            MessageBoxResult primeira_visita = MessageBox.Show("Essa etapa e muito importante para sabermos mais sobre seu empreendimento, preencha todos os campos do formulario.", "Seu FoodTruck!", MessageBoxButton.OK);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            NavigationContext.QueryString.TryGetValue("userId", out Id);
        }

        private void btncadastrar_Click(object sender, EventArgs e)
        {
            Cadastrar();
        }

        public async void Cadastrar()
        {
            DateTime? _datetime = time_abre.Value;
            string hr_abre = _datetime.Value.Hour + ":" + _datetime.Value.Minute;
            
            _datetime = time_fecha.Value;
            string hr_fecha = _datetime.Value.Hour + ":" + _datetime.Value.Minute;

            ListPickerItem selectedItem = (ListPickerItem)lst_segmento.SelectedItem;
            string segmento_lst = (string)selectedItem.Content;
            
            ParseObject cadastro = new ParseObject("Foodtruck");
            try {
                cadastro["idUser"] = Id;
                cadastro["Foodtruck_nome"] = txt_foodtruck_nome.Text;
                cadastro["Segmento"] = segmento_lst;
                cadastro["Hr_abre"] = hr_abre;
                cadastro["Hr_Fecha"] = hr_fecha;
                cadastro["Preco"] = txt_preco.Text;
                cadastro["Telefone"] = txt_telefone.Text;

                GeocodeQuery geocodeQuery = new GeocodeQuery();
                geocodeQuery.GeoCoordinate = new GeoCoordinate();
                geocodeQuery.SearchTerm = txt_local.Text;

                IList<MapLocation> locations = await geocodeQuery.GetMapLocationsAsync();

                foreach (var local in locations)
                {        
                    var point = new ParseGeoPoint(local.GeoCoordinate.Latitude, local.GeoCoordinate.Longitude);
                    cadastro["localizacao"] = point;
                    MessageBoxResult resultado6 = MessageBox.Show("latitude: " + point.Latitude+" longitude: "+ point.Longitude, "Foodtruck Show", MessageBoxButton.OK); 
                }                
                await
                     cadastro.SaveAsync();
            }
            catch (Exception ex) {
                MessageBox.Show("Erro: " + ex);
            }

            MessageBoxResult resultado0 = MessageBox.Show("ID : " + Id, "Foodtruck Show", MessageBoxButton.OK);
            MessageBoxResult resultado1 = MessageBox.Show("Nome : " +txt_foodtruck_nome.Text, "Foodtruck Show", MessageBoxButton.OK);
            MessageBoxResult resultado2 = MessageBox.Show("Segmento : " + segmento_lst, "Foodtruck Show", MessageBoxButton.OK);
            MessageBoxResult resultado3 = MessageBox.Show(hr_abre + " " + hr_fecha , "Foodtruck Show", MessageBoxButton.OK);
            MessageBoxResult resultado4 = MessageBox.Show("Preco : "+ txt_preco.Text, "Foodtruck Show", MessageBoxButton.OK);
            MessageBoxResult resultado5 = MessageBox.Show("tel : "+ txt_telefone.Text, "Foodtruck Show", MessageBoxButton.OK);
            MessageBoxResult resultado = MessageBox.Show("Informacoes cadastradas com sucesso", "Foodtruck Show", MessageBoxButton.OK);

            if (resultado == MessageBoxResult.OK)
                NavigationService.GoBack();
        }
    }
}