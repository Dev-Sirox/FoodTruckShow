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
using System.Device.Location;

namespace FoodTruckShow
{
    public partial class Cadastro : PhoneApplicationPage
    {

        public Cadastro()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (chk_foodtruck.IsChecked == false)
            {
                if ((txt_nome.Text != "") && (txt_email.Text != "") && (txt_senha.Password != ""))
                {
                    verifica_usuario();
                    if (chk_foodtruck.IsChecked == true)
                    {
                        // se for FoodTruck, direciona para outra página com parametros digitados.
                        NavigationService.Navigate(new Uri("/CadastroFoodTruck.xaml?nome=" + txt_nome.Text + "?email=" + txt_email + "?senha=" + txt_senha, UriKind.Relative));
                    }
                }
                else
                {
                    MessageBoxResult resultado = MessageBox.Show("Todos os campos devem ser preenchidos.", "Atenção!", MessageBoxButton.OK);
                }
            }
            else
            {
                NavigationService.Navigate(new Uri("/CadastroFoodTruck.xaml", UriKind.Relative));
            }
        }
        
        private async void verifica_usuario()
        {
            var query = ParseObject.GetQuery("Usuarios").WhereEqualTo("Email", txt_email.Text);
            
            IEnumerable<ParseObject> results = await query.FindAsync(); 
            List<ParseObject> ContactList = results.ToList(); 
            
            if (ContactList.Count == 0)
            {
                Cadastrar();
            }else
            {
                MessageBoxResult resultado = MessageBox.Show("Esse e-mail já está cadastrado em nosso sistema.", "Atenção!", MessageBoxButton.OK);
                if (resultado == MessageBoxResult.OK)
                    txt_email.Text = "";
            }
        }

        public void Cadastrar()
        {
            ParseObject cadastro = new ParseObject("Usuarios");
            cadastro["Nome"] = txt_nome.Text;
            cadastro["Email"] = txt_email.Text;
            cadastro["Senha"] = txt_senha.Password;

            if (chk_foodtruck.IsChecked == true)
            {
                cadastro["foodtruck"] = true;
            }
            else
            {
                cadastro["foodtruck"] = false;

                SystemTray.SetIsVisible(this, true);
                SystemTray.SetOpacity(this, 1);

                ProgressIndicator progresso = new ProgressIndicator
                {
                    IsVisible = true,
                    IsIndeterminate = true,
                    Text = "Aguarde um instante..."
                };

                this.DataContext = null;
                SystemTray.SetProgressIndicator(this, progresso);

                cadastro.SaveAsync();

                MessageBoxResult resultado = MessageBox.Show("Cadastro realizado com sucesso", "Seja Bem-Vindo!", MessageBoxButton.OK);

                if (resultado == MessageBoxResult.OK)
                    NavigationService.GoBack();                   
            }

        }
        

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void chk_foodtruck_Click(object sender, RoutedEventArgs e)
        {
            if(chk_foodtruck.IsChecked == true)
            {
                txt_nome.IsEnabled = false;
                txt_email.IsEnabled = false;
                txt_senha.IsEnabled = false;
            }
            else {
                txt_nome.IsEnabled = true;
                txt_email.IsEnabled = true;
                txt_senha.IsEnabled = true;
            }
        }
    }
}