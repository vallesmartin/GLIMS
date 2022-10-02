using R12.Services;
using R12.ViewModels;
using R12.Views.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace R12.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            StackLayoutNotConnection.IsVisible = !(Repository.IsEndpointReachable());
            FrameConnection.IsVisible = StackLayoutNotConnection.IsVisible;
            await (this.BindingContext as MainViewModel)?.OnAppear();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushAsync(new PendingDeliveriesPage());
        }

        private async void TapGestureRecognizer_Tapped1(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushAsync(new ShipDeliveriesPage());
        }

        private async void TapGestureRecognizer_Tapped2(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushAsync(new DebtsPage());
        }

    }
}