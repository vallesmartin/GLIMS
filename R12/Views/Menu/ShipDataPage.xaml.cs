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
    public partial class ShipDataPage : ContentPage
    {
        public ShipDataPage(long id)
        {
            InitializeComponent();
            BindingContext = new ShipDataViewModel(id);
        }

        protected async override void OnAppearing()
        {
            await (this.BindingContext as ShipDataViewModel)?.Initialize();
        }
    }
}