using R12.Services;
using R12.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace R12
{
    public partial class App : Application
    {
        private static DataStore database;
        public string UserLoggedId;
        public static bool IsEndpointConnected { get; set; }
        public static DataStore Database
        {
            get
            {
                if (database == null)
                {
                    // Ver como reemplazar este hardcodeo con algun diccionario o algo asi...
                    database = new DataStore(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "chiri.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            DependencyService.Register<DataStore>();
            Repository.IsEndpointReachable();
            if (Repository.IsUserLoggedAsync().Result)
            {
                //UserLoggedId = Repository.GetUserLogged().Result;
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new LoginPage();
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
