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
    public class DeliveryLineEditViewModel : BaseViewModel
    {
        public long identifier { get; set; }
        private string packs;
        public string Packs
        {
            get => packs;
            set
            {
                packs = value;
                OnPropertyChanged();
            }
        }
        private ItemModel Item { get; set; }
        private CategoryModel category;
        public CategoryModel Category
        {
            get => category;
            set
            {
                category = value;
                OnPropertyChanged();
            }
        }
        private string manualQty, packsQty;
        public string ManualQty
        {
            get => manualQty;
            set
            {
                manualQty = value;
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
        private DocumentLineModel line;
        public DocumentLineModel Line
        {
            get => line;
            set
            {
                line = value;
                OnPropertyChanged();
            }
        }

        public DeliveryLineEditViewModel(long id)
        {
            Title = "Editar producto en pedido";
            //Task.Run(async () => Line = await Repository.TemporalGetLineByItemAsync(id));
            identifier = id;
            Line = line;
            Category = category;
            ManualSaveLineCommand = new Command(async () => await ManualSaveLine());
            PacksSaveLineCommand = new Command(async () => await PacksSaveLine());
            DeleteCommand = new Command(async () => await EmptyLine());
        }

        public ICommand LoadItemsCommand { get; set; }
        public ICommand SaveLineCommand { get; set; }
        public ICommand ManualSaveLineCommand { get; set; }
        public ICommand PacksSaveLineCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public async Task Initialize()
        {
            Line = await Repository.TemporalGetLineByItemAsync(this.identifier);
            Item = await Repository.Item_GetByIdAsync((int)line.DocumentLineItemId);
            category = await Repository.Category_GetById((int)Item.CategoryId);
            Category = category;
            Packs = 'x' + Item.ItemSuggStep.ToString();
        }
        public async Task PacksSaveLine()
        {
            try
            {
                await SaveLine(Int16.Parse(PacksQty)*Item.ItemSuggStep);
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
        public async Task EmptyLine()
        {
            if (await Shell.Current.DisplayAlert("Eliminar del carrito", "Desea eliminar el articulo del carrito?.", "Si", "Cancelar"))
                await SaveLine(0);

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
        public async Task SaveLine(int? qty)
        {
            await Repository.Temporal_ManageAsync((int)qty, this.Item);
            await Shell.Current.Navigation.PopModalAsync(false);
        }
    }
}
