using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Z_P_B_1.Services;
using Z_P_B_1.ViewModels;
using Z_P_B_1.Views;

namespace Z_P_B_1
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public Database database = App.DataBase;
        public IAccountService Service => DependencyService.Get<IAccountService>();
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(FirstAidItem), typeof(FirstAidItem));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(TaryfikatorsPage), typeof(TaryfikatorsPage));
            Routing.RegisterRoute(nameof(RegPage), typeof(RegPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));

            if (database.GetTokens() == null)
            {
                Shell.Current.GoToAsync("//LoginPage");
            }
        }
        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            if (await database.GetJWT() != null)
            {
                if (await Service.Out())
                    await database.LogOut();
                else
                    return;
            }
            await Shell.Current.GoToAsync("//LoginPage");
        }

        public List<FlyoutItem> GetInDr()
        {
            return new List<FlyoutItem> {InDr,InZd };
        }
    }
}
