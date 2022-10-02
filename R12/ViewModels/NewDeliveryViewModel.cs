using R12.Models;
using R12.Services;
using R12.Views;
using dotMorten.Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace R12.ViewModels
{
    class NewDeliveryViewModel : BaseViewModel
    {
        App app = Xamarin.Forms.Application.Current as App;
        public AutoSuggestBox EntityBox { get; set; }
        public ICommand EntBoxTextChanged { get; set; }
        public ICommand EntBoxQuerySubmitted { get; set; }
        public ICommand EntBoxSuggestionChosen { get; set; }

        private string keyEntity = "NewDelivery_Entity";
        private string entity;
        public Entry qtyEntry = new Entry();
        public ObservableCollection<Andromeda.ItemModel> Items { get; set; }
        public ObservableCollection<Andromeda.DocumentLineModel> DeliveryLine { get; set; }
        public string Entity
        {
            get { return entity; }
            set { entity = value; }
        }
        public NewDeliveryViewModel()
        {
            Title = "NUEVO PEDIDO";

            EntBoxTextChanged = new Command((arg) => {

                Console.WriteLine(arg);

            });

            qtyEntry.Completed += (object sender, EventArgs e) =>
            {
                Console.WriteLine("Completado");
            };
        }


        #region Entity Suggestions
        public async void EntityBox_TextChanged(object sender, dotMorten.Xamarin.Forms.AutoSuggestBoxTextChangedEventArgs args)
        {
            AutoSuggestBox box = (AutoSuggestBox)sender;
            // Only get results when it was a user typing, 
            // otherwise assume the value got filled in by TextMemberPath 
            // or the handler for SuggestionChosen.
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
            var result = await Task.Run<IEnumerable<EntityModel>> (async () =>
            {
                List<EntityModel> suggestions = new List<EntityModel>();
                List<EntityModel> entities = await Repository.Entity_GetAllAsync();
                foreach (var entity in entities)
                {
                    if (entity.EntityLegalName.ToUpper().Contains(text.ToUpper()))
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
            try
            {
                app.Properties.Add(keyEntity, e.QueryText);
            }
            catch (Exception)
            {
                app.Properties.Remove(keyEntity);
                app.Properties.Add(keyEntity, e.QueryText);
            }
        }
        private void EntityBox_SuggestionChosen(object sender, AutoSuggestBoxSuggestionChosenEventArgs e)
        {
            //
        }
        #endregion

    }
}
