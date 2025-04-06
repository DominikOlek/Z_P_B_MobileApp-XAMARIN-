using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Z_P_B_1.Services;
using Z_P_B_1.Views;

namespace Z_P_B_1
{
    public partial class App : Application
    {
        private static Database database;

        public static Database DataBase
        {
            get
            {
                if (database == null)
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"baza.db3"));
                return database;
            }
        }

        private static HttpClient httpClient;

        public static HttpClient HttpClient
        {
            get
            {
                httpClient = httpClient ?? new HttpClient
                (
                    new HttpClientHandler()
                    {
                        ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                        {
                            //bypass
                            return true;
                        },
                    }
                    , false
                )
                {
                    BaseAddress = new Uri("https://192.168.37.80/ApiP/"), //https://10.0.2.2:5001/  https://192.168.0.80/ApiP/
                };
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return httpClient;
            }
        }
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<TaryfikatorsService>();
            DependencyService.Register<AccountService>();
            DependencyService.Register<DriverInfoService>();
            DependencyService.Register<HealthService>();
            MainPage = new AppShell();
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
