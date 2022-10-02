using dotMorten.Xamarin.Forms;
using R12.Models;
using R12.Services;
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
    public partial class FVSCartPage : ContentPage
    {
        public FVSCartPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            await (this.BindingContext as FVSCartViewModel)?.ExecuteLoadCategoriesCommand();
        }

        #region Entity Suggestions
        private async void EntityBox_TextChanged(object sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            Console.WriteLine("EntityBox_TextChanged");
            AutoSuggestBox box = (AutoSuggestBox)sender;
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                if (string.IsNullOrWhiteSpace(box.Text) || box.Text.Length < 3)
                    box.ItemsSource = null;
                else
                {
                    var suggestions = await GetEntitySuggestions(box.Text);
                    box.ItemsSource = suggestions.ToList();
                }
            }
        }
        private async Task<IEnumerable<EntityModel>> GetEntitySuggestions(string text)
        {
            Console.WriteLine("GetEntitySuggestions");
            var result = await Task.Run<IEnumerable<EntityModel>>(async () =>
            {
                List<Models.EntityModel> suggestions = new List<Models.EntityModel>();
                List<Models.EntityModel> entities = await Repository.Entity_GetAllAsync();
                foreach (var entity in entities)
                {
                    if (entity.EntityDescription.ToUpper().Contains(text.ToUpper()))
                    {
                        suggestions.Add(entity);
                    }
                }
                return suggestions;
            });
            return result;
        }
        private void EntityBox_QuerySubmitted(object sender, AutoSuggestBoxQuerySubmittedEventArgs e)
        {
            Console.WriteLine("EntityBox_QuerySubmitted");
        }
        private void EntityBox_SuggestionChosen(object sender, AutoSuggestBoxSuggestionChosenEventArgs e)
        {
            Console.WriteLine("EntityBox_SuggestionChosen");
        }
        #endregion

        private void OnToolbarCancel(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (await DisplayAlert("Advertencia", "Eliminar todos los artículos del carrito ?", "Si", "No"))
                {
                    await (this.BindingContext as FVSCartViewModel)?.Cancel();
                }
            });
        }

        private async void OnToolbarFinish(object sender, EventArgs e)
        {
            if (EntityBox.Text == "")
            {
                await App.Current.MainPage.DisplayAlert("Cliente sin seleccion", "Debe seleccionar un cliente, para eso comience a escribir el código, o nombre en la entrada de texto. El sistema le sugerirá las coincidencias.", "Entendido");
            }
            else
            {
                if (await App.Current.MainPage.DisplayAlert("Finalizar pedido", "El pedido se enviará a preparar", "Si", "Cancelar"))
                {
                    await (this.BindingContext as FVSCartViewModel)?.Finalize();
                }
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Console.WriteLine("TapGestureRecognizer_Tapped");
            Frame categoryFrame = (Frame)sender;
            Grid gridFrameChild = (Grid)categoryFrame.Content;
            Label labelGridChild = (Label)gridFrameChild.Children.ElementAt(0);
            int elementId = Int16.Parse(labelGridChild.Text);
            await (this.BindingContext as FVSCartViewModel)?.OnItemSelected(elementId);
        }
    }
}