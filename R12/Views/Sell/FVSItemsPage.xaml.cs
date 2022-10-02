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
	public partial class FVSItemsPage : ContentPage
	{
        private int identifier { get; set; }
        public FVSItemsPage(int id)
		{
			Console.WriteLine(id.ToString());
			this.identifier = id;
			InitializeComponent();
			BindingContext = new FVSItemsViewModel();
			Task.Run(async () => await (this.BindingContext as FVSItemsViewModel)?.ExecuteLoadItemsCommand(this.identifier));
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();
			await(this.BindingContext as FVSItemsViewModel)?.ExecuteLoadHeaderDataCommand(this.identifier);
		}

		protected async override void OnDisappearing()
        {
			Console.WriteLine("FVSItemsPage OnDisappearing");
        }

		private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
			Console.WriteLine("TapGestureRecognizer_Tapped");
			Frame categoryFrame = (Frame)sender;
			Grid gridFrameChild = (Grid)categoryFrame.Content;
			Label labelGridChild = (Label)gridFrameChild.Children.ElementAt(1);
			int elementId = Int16.Parse(labelGridChild.Text);
			await (this.BindingContext as FVSItemsViewModel)?.OnItemSelected(elementId);
		}
    }
}