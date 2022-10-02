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
    public class FastVisualSellViewModel : BaseViewModel
    {
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

        private string entity;
        public string Entity
        {
            get { return entity; }
            set { entity = value; }
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
        public ObservableCollection<CategoryModel> Categories { get; set; }

        public FastVisualSellViewModel()
        {
            Title = "";
            Task.Run(async () => ItemsCount = await Repository.Temporal_GetCountLinesAsync());
            CartIcon = new FileImageSource { File = "cart.png" };
            Categories = new ObservableCollection<CategoryModel>();
            LoadCategoriesCommand = new Command(async () => await ExecuteLoadCategoriesCommand());
            CartCommand = new Command(async () => await GoToCart());
        }

        private async Task GoToCart()
        {
            await Shell.Current.Navigation.PushAsync(new FVSCartPage());
        }

        public async Task OnFrameTapped(int id)
        {
            await App.Current.MainPage.Navigation.PushAsync( new FVSItemsPage(id), false );
        }

        public ICommand CartCommand { get; set; }
        public ICommand LoadCategoriesCommand { get; set; }

        public async Task ExecuteLoadCategoriesCommand()
        {
            Categories.Clear();
            var cats = await Repository.Category_GetAllAsync();
            var catsOrdered = cats.OrderBy(c => c.CategoryOrder);
            foreach (var cat in catsOrdered)
                Categories.Add(cat);
            Task.Run(async () => ItemsCount = await Repository.Temporal_GetCountLinesAsync());
        }
    }
}