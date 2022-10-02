using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Linq;
using R12.Services;
using R12.Views;
using System.Collections.Generic;
using R12.Models;
using R12.Views.Menu;
using System.Collections.ObjectModel;

namespace R12.ViewModels
{
    public class ShipDataViewModel : BaseViewModel
    {
        private long Id;
        public string Amount { get; set; }
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

        private DocumentModel delivery;
        public DocumentModel Delivery
        {
            get => delivery;
            set
            {
                delivery = value;
                OnPropertyChanged();
            }
        }

        private List<DocumentLineModel> detail;
        public List<DocumentLineModel> Detail
        {
            get => detail;
            set
            {
                detail = value;
                OnPropertyChanged();
            }
        }

        public ShipDataViewModel(long id)
        {
            Title = "Entrega en cliente";
            Id = id;
            Delivery = new DocumentModel();
            SignCommand = new Command(async () => await Sign());
            FinalizeCommand = new Command(async () => await Finalize());
        }

        public ICommand ExecuteLoadCommand { get; set; }
        public ICommand SignCommand { get; set; }
        public ICommand FinalizeCommand { get; set; }

        public async Task Initialize()
        {
            Delivery = await Repository.Delivery_GetByIdAsync(Id);
        }
        public async Task Sign()
        {
            if (Amount != null)
            {
                if (await Shell.Current.DisplayAlert("Advertencia", "La entrega se marcará como Firmada", "Firmar", "Cancelar"))
                {
                    var document = new DocumentModel { DocumentId = Id, DocumentTotalAmount = float.Parse(Amount) };
                    await Repository.Delivery_SetSigned(document);
                    await Shell.Current.Navigation.PopAsync();
                }
            }else{
                await Shell.Current.DisplayAlert("Advertencia", "Debe incluir un importe", "Entendido");
            }
        }
        public async Task Finalize()
        {
            if (Amount != null)
            {
                if (await Shell.Current.DisplayAlert("Advertencia", "La entrega se informara como Enviada/Cobrada", "Entendido", "Cancelar"))
                {
                    var document = new DocumentModel { DocumentId = Id, DocumentTotalAmount = float.Parse(Amount) };
                    await Repository.Delivery_SetDelivered(document);
                    await Shell.Current.Navigation.PopAsync();
                }
            }
            else 
            {
                await Shell.Current.DisplayAlert("Advertencia", "Debe incluir un importe", "Entendido");
            }
        }
        public async Task Cancel()
        {            
            await Repository.Delivery_DeleteByIdAsync(this.Id);
            await Shell.Current.Navigation.PopAsync();
        }
        internal async Task OnItemSelected(int elementId)
        {
            Console.WriteLine("OnFrameTapped " + elementId.ToString());
            //var page = new DeliveryLineEditPage(e;
            await App.Current.MainPage.Navigation.PushAsync(new DeliveryLineEditPage(elementId), true); ;
        }
    }
}