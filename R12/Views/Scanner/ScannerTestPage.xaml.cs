using R12.Models;
using R12.Services;
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
    public partial class ScannerTestPage : ContentPage
    {
        public ScannerTestPage()
        {
            InitializeComponent();
        }

        private async void _scanView_OnScanResult(ZXing.Result result)
        {
            ItemModel promiseItem = new ItemModel();
            try
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    try
                    {
                        promiseItem = await Repository.Item_GetByBarcodeAsync(result.Text);
                        
                        if (promiseItem != null)
                        {
                            await Shell.Current.GoToAsync("//Main");
                            await Shell.Current.Navigation.PushAsync(new ItemPage((int)promiseItem.ItemId));
                        }
                        else
                        {
                            await DisplayAlert(result.Text, "Codigo no encontrado", "Entendido");
                        }
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("2", ex.Message, "Entendido");
                    }
                });
            }
            catch (Exception ex)
            {
                await DisplayAlert("1", ex.Message, "Entendido");
            }
            
        }
    }
}