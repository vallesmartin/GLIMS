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
    public partial class LoggingPage : ContentPage
    {
        public LoggingPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await (this.BindingContext as LoggingViewModel)?.ExecuteLoadItemsCommand();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Frame logFrame = (Frame)sender;
            StackLayout logStack = (StackLayout)logFrame.Content;
            Grid logGrid = (Grid)logStack.Children[0];
            Label logMessageIdLabel = (Label)logGrid.Children[0];
            long logMessageId = Convert.ToInt64(logMessageIdLabel.Text);
            await (this.BindingContext as LoggingViewModel)?.SendLogAsync(logMessageId);
            /*Console.WriteLine("TapGestureRecognizer_Tapped");
            Frame categoryFrame = (Frame)sender;
            Grid gridFrameChild = (Grid)categoryFrame.Content;
            Label labelGridChild = (Label)gridFrameChild.Children.ElementAt(1);
            int elementId = Int16.Parse(labelGridChild.Text);
            await (this.BindingContext as FVSItemsViewModel)?.OnItemSelected(elementId);*/
        }
    }
}