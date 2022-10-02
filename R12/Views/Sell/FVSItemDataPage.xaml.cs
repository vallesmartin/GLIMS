using R12.ViewModels;
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
    public partial class FVSItemDataPage : ContentPage
    {
        public FVSItemDataPage(int id)
        {
            InitializeComponent();
            BindingContext = new FVSItemDataViewModel(id);
        }

        protected async override void OnAppearing()
        {
            await (this.BindingContext as FVSItemDataViewModel)?.Initialize();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Shell.Current.Navigation.PopModalAsync();
        }
    }
}