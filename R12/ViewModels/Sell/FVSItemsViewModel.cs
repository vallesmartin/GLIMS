using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Linq;
using R12.Services;
using R12.Views;
using R12.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;

namespace R12.ViewModels
{
    public class FVSItemsViewModel : BaseViewModel
    {
        public string CatDescription;
        private FileImageSource cartIcon;
        public FileImageSource CartIcon
        {
            get => cartIcon;
            set
            {
                cartIcon = value;
                OnPropertyChanged();
            }
        }

        private int itemsCount;
        public int ItemsCount
        {
            get => itemsCount;
            set
            {
                itemsCount = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ItemModel> Items { get; set; }

        public FVSItemsViewModel()
        {
            Title = "";
            Task.Run(async () => ItemsCount = await Repository.Temporal_GetCountLinesAsync());
            CartIcon = new FileImageSource { File = "cart.png" };
            Items = new ObservableCollection<ItemModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(0));
            CartCommand = new Command(async () => await GoToCart());
        }

        public async Task OnItemSelected(int id)
        {
            await Shell.Current.Navigation.PushModalAsync(new FVSItemDataPage(id), false);
        }

        private async Task GoToCart()
        {
            await Shell.Current.Navigation.PopAsync();
            await Shell.Current.Navigation.PushAsync(new FVSCartPage());
        }

        public ICommand LoadItemsCommand { get; set; }
        public ICommand CartCommand { get; set; }

        public async Task ExecuteLoadHeaderDataCommand(int id)
        {
            Task.Run(async () => ItemsCount = await Repository.Temporal_GetCountLinesAsync());
        }
        
        public async Task ExecuteLoadItemsCommand(int id)
        {
            Items.Clear();
            var category = await Repository.Category_GetById(id);
            Title = category.CategoryDescription;
            var items = await Repository.Item_GetByCategoryAsync(id);
            var itemsOrdered = items.OrderBy(i => i.ItemOrder);
            foreach (var item in itemsOrdered)
                Items.Add(item);
            Task.Run(async () => ItemsCount = await Repository.Temporal_GetCountLinesAsync());
        }
    }
}