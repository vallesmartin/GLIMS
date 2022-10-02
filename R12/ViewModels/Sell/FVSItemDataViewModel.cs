using R12.Models;
using R12.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace R12.ViewModels
{
    public class FVSItemDataViewModel : BaseViewModel
    {
        public int identifier { get; set; }
        private string manualQty;
        public string ManualQty
        {
            get => manualQty;
            set
            {
                manualQty = value;
                OnPropertyChanged();
            }
        }
        private string packs, packsQty;
        public string Packs
        {
            get => packs;
            set
            {
                packs = value;
                OnPropertyChanged();
            }
        }
        public string PacksQty
        {
            get => packsQty;
            set
            {
                packsQty = value;
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
        private ItemModel item;
        public ItemModel Item
        {
            get => item;
            set
            {
                item = value;
                OnPropertyChanged();
            }
        }

        public FVSItemDataViewModel(int id)
        {
            identifier = id;
            Title = "Agregar artículo";
            CartIcon = new FileImageSource { File = "cart.png" };
            //Task.Run(async () => ItemsCount = await Repository.Temporal_GetCountLinesAsync());
            //Task.Run(async () => Item = await Repository.Item_GetByIdAsync(id));
            ManualSaveLineCommand = new Command(async () => await ManualSaveLine());
            PacksSaveLineCommand = new Command(async () => await PacksSaveLine());
        }

        public ICommand LoadItemsCommand { get; set; }
        public ICommand SaveLineCommand { get; set; }
        public ICommand ManualSaveLineCommand { get; set; }
        public ICommand PacksSaveLineCommand { get; set; }

        public async Task Initialize()
        {
            ItemsCount = await Repository.Temporal_GetCountLinesAsync();
            Item = await Repository.Item_GetByIdAsync(this.identifier);
            Packs = 'x' + Item.ItemSuggStep.ToString();
        }
        public async Task FastLowSaveLine()
        {
            Console.WriteLine("FastLowSaveLine!");
            await SaveLine(Item.ItemSuggLow);
        }
        public async Task FastSaveLine()
        {
            Console.WriteLine("FastSavedLine!");
            await SaveLine(Item.ItemSugg);
        }
        public async Task FastHighSaveLine()
        {
            Console.WriteLine("FastHighSaveLine!");
            await SaveLine(Item.ItemSuggHigh);
        }
        public async Task ManualSaveLine()
        {
            try
            {
                await SaveLine(Int16.Parse(ManualQty));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }            
        }
        public async Task PacksSaveLine()
        {
            try
            {
                await SaveLine(Int16.Parse(PacksQty) * Item.ItemSuggStep);
            }
            catch (Exception ex)
            {
                try
                {
                    await SaveLine(1 * Item.ItemSuggStep);
                }
                catch (Exception)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine(ex.Message);
            }
        }
        public async Task SaveLine(int? qty)
        {
            Console.WriteLine("SavedLine!");
            await Repository.Temporal_ManageAsync((int)qty, Item);
            await Shell.Current.Navigation.PopModalAsync(false);
        }
    }
}
