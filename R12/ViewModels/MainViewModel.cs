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
using R12.Views;
using R12.Views.Menu;
using System.Collections.ObjectModel;

namespace R12.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private FileImageSource sendLogo, recycleLogo;
        private string deliveriesPending, countShip, countDebts;
        private ObservableCollection<DeliveryModel> deliveries;
        private DocumentModel lastDocument;
        private int countPendingDeliveries, countToday, countDeliveries;
        private Color countPendingColor, countShipColor, countDebtColor;
        public FileImageSource SendLogo
        {
            get => sendLogo;
            set
            {
                sendLogo = value;
                OnPropertyChanged();
            }
        }
        public FileImageSource RecycleLogo
        {
            get => recycleLogo;
            set
            {
                recycleLogo = value;
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
        public ObservableCollection<DeliveryModel> Deliveries
        {
            get => deliveries;
            set
            {
                deliveries = value;
                OnPropertyChanged();
            }
        }
        public DocumentModel LastDocument
        {
            get => lastDocument;
            set
            {
                lastDocument = value;
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
        public string CountShip
        {
            get => countShip;
            set
            {
                countShip = value;
                OnPropertyChanged();
            }
        }
        public string CountDebts
        {
            get => countDebts;
            set
            {
                countDebts = value;
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
        public int CountToday
        {
            get => countToday;
            set
            {
                countToday = value;
                OnPropertyChanged();
            }
        }
        public Color CountPendingColor
        {
            get => countPendingColor;
            set
            {
                countPendingColor = value;
                OnPropertyChanged();
            }
        }
        public Color CountShipColor
        {
            get => countShipColor;
            set
            {
                countShipColor = value;
                OnPropertyChanged();
            }
        }
        public Color CountDebtColor
        {
            get => countDebtColor;
            set
            {
                countDebtColor = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            Title = "";
            //Task.Run(async () => countDeliveries = await Repository.Delivery_GetCountPendingAsync());
            //DeliveriesPending = countDeliveries.ToString();
            SendLogo = new FileImageSource { File = "enviapedido.png" };
            RecycleLogo = new FileImageSource { File = "recycle.png" };
            RefreshCommand = new Command(async () => await OnRefresh());
            RefreshPageCommand = new Command(async () => await RefreshPage());
            SendDeliveryCommand = new Command(async () => await SendDelivery());
            RecycleCommand = new Command(async () => await Recycle());
            GoToSellCommand = new Command(async () => await GoToSell());
            GoToScanCommand = new Command(async () => await Scan());
            DeleteDeliveriesCommand = new Command(async () => await DeleteDeliveriesAsync());
        }
        public ICommand MainPlusCommand { get; }
        public ICommand SendDeliveryCommand { get; }
        public ICommand RecycleCommand { get; }
        public ICommand TapCustonViewCommand { get; }
        public ICommand FastSellInitCommand { get; }
        public ICommand GoToSellCommand { get; }
        public ICommand GoToScanCommand { get; }
        public ICommand DeleteDeliveriesCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand RefreshPageCommand{ get; set; }

        public async Task SendDelivery()
        {
            if (await Shell.Current.DisplayAlert("Andromeda", "Enviar pedidos en cola?", "Enviar", "Cancelar"))
            {
                ApiResponse result = await Repository.Andromeda_TrySendPendingAsync();
                await Shell.Current.DisplayAlert("Envio de pedidos", result.Message, "Entendido");
            }
            await OnAppear();
        }
        public async Task Recycle()
        {
            try
            {
                if (await Shell.Current.DisplayAlert("Actualizar Datos", "Se actualizarán Artículos y Clientes con la base de datos remota en línea. Puede tardar varios segundos.", "Entiendo, actualizar.", "Cancelar"))
                    await Repository.Andromeda_SyncAsync();
                await OnAppear();
            }
            catch (Exception)
            {
            }
            
        }
        public async Task OnAppear()
        {
            // capa de reintentos
            try
            {
                await Task.Delay(600);
                await LoadDataScreen();
            }
            catch (Exception)
            {
                try
                {
                    await Task.Delay(600);
                    await LoadDataScreen();
                }
                catch (Exception)
                {
                    await Task.Delay(1200);
                    await LoadDataScreen();
                }
            }
        }
        private async Task LoadDataScreen() 
        { 
            await Task.Run(async () =>
            {
                Deliveries = new ObservableCollection<DeliveryModel>(await Repository.Delivery_GetAllAsync());
                CountDeliveries = Deliveries.Count;
                CountPendingDeliveries = Deliveries.Where(d => d._DocumentStatus == 0).Count();
                CountToday = Deliveries.Where(d => d._DocumentStatus > 0 && d._DocumentDate.Value.Date == DateTime.Now.Date).Count();

                CountShip = (await Repository.Delivery_GetByStatusAsync(20)).Count.ToString();
                CountDebts = (await Repository.Delivery_GetByStatusAsync(31)).Count.ToString();
                CountPendingColor = (CountPendingDeliveries > 0) ? (Color)Application.Current.Resources["Warning"] : (Color)Application.Current.Resources["TextSuccess"];
                if (Repository.IsEndpointReachable())
                {
                    CountShip = (await Repository.Delivery_GetByStatusAsync(20)).Count.ToString();
                    CountDebts = (await Repository.Delivery_GetByStatusAsync(31)).Count.ToString();
                    CountShipColor = (Int16.Parse(CountShip) == 0) ? (Color)Application.Current.Resources["TextPrimary"] : (Color)Application.Current.Resources["TextSuccess"];
                    CountDebtColor = (Int16.Parse(CountDebts) == 0) ? (Color)Application.Current.Resources["TextPrimary"] : (Color)Application.Current.Resources["TextSuccess"];
                }
                else
                {
                    CountShip = "sin datos";
                    CountDebts = "sin datos";
                    CountShipColor = (Color)Application.Current.Resources["Warning"];
                    CountDebtColor = (Color)Application.Current.Resources["Warning"];
                }
            });
            await Task.Run( () => LastDocument = Repository.Delivery_Remote_GetLast().Result);
        }
        public async Task OnRefresh()
        {
            await OnAppear();
        }
        private async Task DeleteDeliveriesAsync()
        {
            if (await App.Current.MainPage.DisplayAlert("Eliminar cola", "Se eliminaran pedidos pendientes", "Eliminar", "Cancelar"))
            {
                await Repository.Delivery_DeleteAll();
                await OnAppear();
            }
        }
        private async Task Scan()
        {
            await Shell.Current.Navigation.PushAsync(new ScannerTestPage(), true);
        }
        private async Task GoToSell()
        {
            await Shell.Current.GoToAsync("//New");
        }
        private async Task RefreshPage()
        {
            await Shell.Current.Navigation.PushModalAsync(new ConnModalPage(), false);
        }
    }
}