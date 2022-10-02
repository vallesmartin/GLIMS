using R12.ViewModels;
using R12.Views;
using R12.Views.Menu;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace R12
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainPage) + '/' + nameof(PendingDeliveriesPage), typeof(PendingDeliveriesPage));
            //Routing.RegisterRoute(nameof(MainPage) + '/' + nameof(FastVisualSellPage), typeof(FastVisualSellPage));
            //Routing.RegisterRoute(nameof(MainPage) + '/' + nameof(FastVisualSellPage) + '/' + nameof(FVSItemsPage), typeof(FVSItemsPage));
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
