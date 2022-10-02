using R12.Views;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Linq;
using R12.Common;
using R12.Services;

namespace R12.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public string User { get; set; }
        public string Password { get; set; }

        public LoginViewModel()
        {
            Title = "";
            LoginCommand = new Command(async () => await LoginAsync());
            NavRegisterCommand = new Command(GoToRegistrationPage);
        }

        public ICommand NavigationList { get; set; }
        public ICommand LoginCommand { get; }
        public ICommand NavRegisterCommand { get; }

        public async Task LoginAsync()
        {
            APIResponse response = await Repository.LoginAsync(new Andromeda.LoginRequestModel { Username = User, Password = Password });
            if (response.IsOk)
            {
                /*Application.Current.MainPage = new NavigationPage();
                await Application.Current.MainPage.Navigation.PushAsync(new MainPage());*/
                App.Current.MainPage = new AppShell();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login", response.Message, "Entendido");
            }
        }
        public void GoToRegistrationPage()
        {
            //Application.Current.MainPage = new RegisterPage();
        }
    }
}