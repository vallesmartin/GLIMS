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
    public partial class PendingDeliveryPage : ContentPage
    {
        public PendingDeliveryPage(long id)
        {
            InitializeComponent();
            BindingContext = new PendingDeliveryViewModel(id);
        }

        protected async override void OnAppearing()
        {
            await (this.BindingContext as PendingDeliveryViewModel)?.Initialize();
        }

        private void OnToolbarCancel(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (await Shell.Current.DisplayAlert("Advertencia", "Se eliminará el pedido del telefono.", "Eliminar", "Cancelar"))
                {
                    await (this.BindingContext as PendingDeliveryViewModel)?.Cancel();
                }
            });
        }

        private async void OnToolbarFinish(object sender, EventArgs e)
        {
        }
    }
}