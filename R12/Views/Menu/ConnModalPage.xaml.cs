using R12.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace R12.Views.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConnModalPage : ContentPage
    {
        public ConnModalPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            LabelReconn.Text = "Reconectando";
            await Task.Delay(500);
            LabelReconn.Text = "Reconectando .";
            await Task.Delay(500);
            LabelReconn.Text = "Reconectando ..";
            await Task.Delay(500);
            LabelReconn.Text = "Reconectando ...";
            bool connected = false;
            connected = Repository.IsEndpointReachable();
            BtnAccept.Text = connected ? "CONTINUAR" : "VOLVER";
            BtnAccept.TextColor = connected ? Color.Green : Color.Red;
            BtnAccept.HeightRequest = 45;
            LabelReconn.Text = connected ? "Conexión exitosa" : "Sin conexión. Verifique si su teléfono posee conexión a internet y vuelva a intentar.";
        }

        private void BtnAccept_Released(object sender, EventArgs e)
        {
            Shell.Current.Navigation.PopModalAsync();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Shell.Current.Navigation.PopModalAsync();
        }
    }
}