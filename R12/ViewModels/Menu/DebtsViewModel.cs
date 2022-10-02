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
    public class DebtsViewModel : BaseViewModel
    {
        private FileImageSource trashLogo, sendLogo;
        private string deliveriesPending;
        private ObservableCollection<DocumentModel> deliveries;
        private bool isPending;
        private int countPendingDeliveries;
        private int countDeliveries;
        public FileImageSource TrashLogo
        { 
            get => trashLogo;
            set
            {
                trashLogo = value;
                OnPropertyChanged();
            }
        }
        public FileImageSource SendLogo
        {
            get => sendLogo;
            set
            {
                sendLogo = value;
                OnPropertyChanged();
            }
        }
        public string DeliveriesPending
        {
            get => deliveriesPending;
            set
            {
                deliveriesPending = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<DocumentModel> Deliveries
        {
            get => deliveries;
            set
            {
                deliveries = value;
                OnPropertyChanged();
            }
        }
        public int CountDeliveries
        {
            get => countDeliveries;
            set
            {
                countDeliveries = value;
                OnPropertyChanged();
            }
        }
        public int CountPendingDeliveries
        {
            get => countPendingDeliveries;
            set
            {
                countPendingDeliveries = value;
                OnPropertyChanged();
            }
        }
        public bool IsPending
        {
            get => isPending;
            set
            {
                isPending = value;
                OnPropertyChanged();
            }
        }

        public DebtsViewModel()
        {
            Title = "Cobranzas";
            IsPending = false;
            Task.Run(async () => countDeliveries = await Repository.Delivery_GetCountPendingAsync());
            DeliveriesPending = countDeliveries.ToString();
            SendLogo = new FileImageSource { File = "enviapedido.png" };
            TrashLogo = new FileImageSource { File = "trash.png" };
            SendDeliveryCommand = new Command(async () => await SendDelivery());
            RefreshPageCommand = new Command(async () => await RefreshPage());
            DeleteDeliveriesCommand = new Command(async () => await DeleteDeliveriesAsync());
        }

        public ICommand MainPlusCommand { get; }
        public ICommand SendDeliveryCommand { get; }
        public ICommand RecycleCommand { get; }
        public ICommand TapCustonViewCommand { get; }
        public ICommand FastSellInitCommand { get; }
        public ICommand ManualSaveLineCommand { get; }
        public ICommand DeleteDeliveriesCommand { get; set; }
        public ICommand RefreshPageCommand { get; set; }

        public async Task OnAppear()
        {
            await Task.Run(async () => {
                // Obtiene observable
                Deliveries = new ObservableCollection<DocumentModel>(await Repository.Delivery_GetByStatusAsync(31));
                // Ordena y cuenta
                Deliveries = new ObservableCollection<DocumentModel>(Deliveries.OrderBy(d => d.DocumentStatus).ThenBy(d => d.DocumentDate).ToList());
                CountDeliveries = Deliveries.Count;
                var DelPending = Deliveries.Where(d => d.DocumentStatus == 0).ToList();
                CountPendingDeliveries = DelPending.Count();
                IsPending = CountPendingDeliveries > 0;
            });
        }
        public async Task OnFrameTapped(long id)
        {
            await App.Current.MainPage.Navigation.PushAsync(new ShipDataPage(id));
        }
        public async Task SendDelivery()
        {
            if (await Shell.Current.DisplayAlert("Enviar pendientes", "Se enviarán los pedidos pendientes.", "Enviar", "Cancelar"))
            {
                ApiResponse result = await Repository.Andromeda_TrySendPendingAsync();
                await Shell.Current.DisplayAlert("Envio de pedidos", result.Message, "Entendido");
            }
            await OnAppear();
        }
        private async Task DeleteDeliveriesAsync()
        {
            if (await App.Current.MainPage.DisplayAlert("Advertencia", "Se eliminarán todos los pedidos del teléfono. Incluidos los no enviados.", "Entiendo, eliminar", "Cancelar"))
            {
                await Repository.Delivery_DeleteAll();
                await OnAppear();
            }
        }
        private async Task RefreshPage()
        {
            await Shell.Current.Navigation.PushModalAsync(new ConnModalPage(), false);
        }
    }
}