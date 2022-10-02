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
	public partial class ItemPage : ContentPage
	{
        public int identifier { get; set; }
        public ItemPage (int id)
		{
			this.identifier = id;
			InitializeComponent ();
			BindingContext = new ItemViewModel(id);
		}
		protected async override void OnAppearing()
		{
			await (this.BindingContext as ItemViewModel)?.OnAppear();
		}
	}
}