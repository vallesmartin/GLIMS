using R12.Models;
using R12.Services;
using R12.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace R12.ViewModels
{
    public class FVSCartViewModel : BaseViewModel
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

        public ObservableCollection<DocumentLineModel> DocumentLines { get; set; }

        public FVSCartViewModel()
        {
            Title = "Carrito";
            DocumentLines = new ObservableCollection<DocumentLineModel>();
            ExecuteLoadCommand = new Command(async () => await ExecuteLoadCategoriesCommand());
            CancelCommand = new Command(async () => await Cancel());
            FinalizeCommand = new Command(async () => await Finalize());
        }
        public ICommand ExecuteLoadCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand FinalizeCommand { get; set; }

        public async Task Finalize()
        {
            EntityModel entityModel = new EntityModel();
            bool isOkEntity = false;
            var Ent = await Repository.Entity_GetAllAsync();
            foreach(var e in Ent)
            {
                if(e.EntityDescription == Entity)
                {
                    entityModel = e;
                    isOkEntity = true;
                }
            }
            if (isOkEntity)
            {
                ApiResponse result = await Repository.Andromeda_TrySendDocumentAsync(entityModel);
                if(result.isOk)
                    await Shell.Current.DisplayAlert("Pedido enviado", "Se generó el pedido en Andrómeda #"+result.NumericMessage.ToString().Trim(), "Entendido");
                else
                    await Shell.Current.DisplayAlert("Error de conexión", "El pedido fue guardado en el teléfono. Intente enviarlo desde el Menú de Inicio.", "Entendido");
                await Repository.Temporal_DeleteAsync();
                await Shell.Current.Navigation.PopAsync();
            }
            else
            {
                await Shell.Current.DisplayAlert("Seleccion de clientes", "Debe seleccionar un cliente, para eso comience a escribir el codigo, o nombre en la entrada de texto. El sistema le sugerira las coincidencias.", "Entendido");
            }
        }
        public async Task Cancel()
        {
            await Repository.Temporal_DeleteAsync();
            await Shell.Current.Navigation.PopAsync();
        }
        public async Task ExecuteLoadCategoriesCommand()
        {
            DocumentLines.Clear();
            var lines = await Repository.Temporal_GetLinesAsync();
            foreach (var line in lines)
                DocumentLines.Add(line);
        }
        internal async Task OnItemSelected(int elementId)
        {
            Console.WriteLine("OnFrameTapped "+ elementId.ToString());
            //var page = new DeliveryLineEditPage(e;
            await App.Current.MainPage.Navigation.PushModalAsync(new DeliveryLineEditPage(elementId), false); ;
        }
    }
}
