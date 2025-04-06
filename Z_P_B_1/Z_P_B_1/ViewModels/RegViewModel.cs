using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Z_P_B_1.Models;
using Z_P_B_1.Services;
using Z_P_B_1.Views;

namespace Z_P_B_1.ViewModels
{
    public class RegViewModel : BaseViewModel
    {
        public IAccountService Service => DependencyService.Get<IAccountService>();
        public Command LoginCommand { get; }

        public RegViewModel()
        {
            LoginCommand = new Command(GoLog);
        }

        private async void GoLog()
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            Application.Current.MainPage = new AppShell();
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        public async Task<bool> Register(string info)
        {
            if (await Service.Reg(info)) {
                GoLog();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
    
}