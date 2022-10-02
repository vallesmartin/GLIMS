using dotMorten.Xamarin.Forms;
using R12.Models;
using R12.Services;
using R12.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace R12.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FastVisualSellPage : ContentPage
    {
        //public ICommand OnInputCommand { get; set; }
        public FastVisualSellPage()
        {
            InitializeComponent();
            //OnInputCommand = new Command(async () => await InputButton());
        }

        protected async override void OnAppearing()
        {
            await (BindingContext as FastVisualSellViewModel)?.ExecuteLoadCategoriesCommand();
            if (ItemBox != null)
            {
                ItemBox.Text = "";
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Frame categoryFrame = (Frame)sender;
            Grid gridFrameChild = (Grid)categoryFrame.Content;
            Label labelGridChild = (Label)gridFrameChild.Children.ElementAt(2);
            int elementId = Int16.Parse(labelGridChild.Text);
            await (this.BindingContext as FastVisualSellViewModel)?.OnFrameTapped(elementId);
        }

        #region Item Suggestions
        private async void ItemBox_TextChanged(object sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            AutoSuggestBox box = (AutoSuggestBox)sender;
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                if (string.IsNullOrWhiteSpace(box.Text) || box.Text.Length < 3)
                    box.ItemsSource = null;
                else
                {
                    var suggestions = await GetItemSuggestions(box.Text);
                    box.ItemsSource = suggestions.ToList();
                }
            }
        }
        private async Task<IEnumerable<ItemModel>> GetItemSuggestions(string text)
        {
            var result = await Task.Run<IEnumerable<ItemModel>>(async () =>
            {
                List<Models.ItemModel> suggestions = new List<Models.ItemModel>();
                List<Models.ItemModel> items = await Repository.Item_GetAllAsync();
                foreach (var item in items)
                {
                    if (item.ItemInternalDescription.ToUpper().Contains(text.ToUpper()))
                    {
                        suggestions.Add(item);
                    }
                }
                return (IEnumerable<ItemModel>)suggestions;
            });
            return result;
        }
        private async void ItemBox_QuerySubmitted(object sender, AutoSuggestBoxQuerySubmittedEventArgs e)
        {
            ItemModel itemChosen = (ItemModel)e.ChosenSuggestion;
            if (itemChosen != null)
            {
                await Shell.Current.Navigation.PushModalAsync(new FVSItemDataPage((int)itemChosen.ItemId), false);
                
            }
        }
        private void ItemBox_SuggestionChosen(object sender, AutoSuggestBoxSuggestionChosenEventArgs e)
        {
        }
        #endregion

        private void Button_Clicked(object sender, EventArgs e)
        {
            Focus();
        }

        private void Focus()
        {
            DummyEntry.Focus();
            ItemBox.Focus();
            ItemBox.Focus();
        }
    }
}