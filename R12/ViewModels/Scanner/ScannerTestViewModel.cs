using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace R12.ViewModels
{
    public class ScannerTestViewModel : BaseViewModel
    {
        private string scanResult;
        public string ScanResult {
            get => scanResult;
            set
            {
                scanResult = value;
                OnPropertyChanged();
            }
        }

        public ScannerTestViewModel()
        {
            Title = "Escaner de Articulos";
            ScanResultCommand = new Command(async () => await Scan());
        }

        public ICommand ScanResultCommand { get; }

        public async Task Scan()
        {
            await Shell.Current.DisplayAlert("Scanner","ViewModel","OK");
        }
        public async Task OnAppear()
        {
            
        }
    }
}
