using R12.Models;
using R12.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace R12.ViewModels
{
    public class LoggingViewModel : BaseViewModel
    {
        public ObservableCollection<LogMessageModel> Logs { get; set; }

        public LoggingViewModel()
        {
            Title = "Información de errores";
            Logs = new ObservableCollection<LogMessageModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            DeleteCommand = new Command(async () => await DeleteAsync());
        }

        private async Task DeleteAsync()
        {
            if (await App.Current.MainPage.DisplayAlert("Advertencia", "Se eliminarán todos los logs.", "Entiendo, eliminar", "Cancelar"))
            {
                await Repository.Logging_DeleteAll();
                await ExecuteLoadItemsCommand();
            }
        }

        public ICommand LoadItemsCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public async Task ExecuteLoadItemsCommand()
        {
            Logs.Clear();
            var logs = await Repository.Log_GetAllAsync();
            foreach (var log in logs)
                Logs.Add(log);
        }

        public async Task SendLogAsync(long id)
        {
            if (await App.Current.MainPage.DisplayAlert("Enviar información", "Se informará el error al servidor remoto.", "Enviar", "Cancelar"))
            {
                var log = await Repository.Log_GetAsyn(id);
                await Repository.Log_SendAsync(log);
                await ExecuteLoadItemsCommand();
            }
        }
    }
}
