using R12.Services;
using R12.ViewModels;
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
    public partial class ShipDeliveriesPage : ContentPage
    {
        public ShipDeliveriesPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            StackLayoutNotConnection.IsVisible = !(Repository.IsEndpointReachable());
            FrameConnection.IsVisible = StackLayoutNotConnection.IsVisible;
            await (this.BindingContext as ShipDeliveriesViewModel)?.OnAppear();
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Frame childrenFrame = (Frame)sender;
            StackLayout StackChild = (StackLayout)childrenFrame.Content;
            Label labelDocuemntIdChild = (Label)StackChild.Children.ElementAt(0); //el primero
            long elementId = long.Parse(labelDocuemntIdChild.Text);
            await (this.BindingContext as ShipDeliveriesViewModel)?.OnFrameTapped(elementId);
        }
    }
}