using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Z_P_B_1.Models;
using Z_P_B_1.Services;
using Z_P_B_1.Views;

namespace Z_P_B_1.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public Command WithoutLog { get; }
        public Command SendCode { get; }
        public Command ChangeCommand { get; }
        public Command GoChange { get; }
        public Command EndChange { get; }
        public IAccountService Service => DependencyService.Get<IAccountService>();
        string email = "";
        public string Email { get { return email; } set { email = value; } }

        bool mainVisible = true;
        public bool MainVisible
        {
            get { return mainVisible; }
            set { SetProperty(ref mainVisible, value); }
        }
        bool bgVisible = false;
        public bool BgVisible
        {
            get { return bgVisible; }
            set { SetProperty(ref bgVisible, value); }
        }
        bool emailVisible = false;
        public bool EmailVisible
        {
            get { return emailVisible; }
            set { SetProperty(ref emailVisible, value); }
        }
        bool changeVisible = false;
        public bool ChangeVisible
        {
            get { return changeVisible; }
            set { SetProperty(ref changeVisible, value); }
        }

        string error = string.Empty;
        public string Error
        {
            get { return error; }
            set { SetProperty(ref error, value); }
        }

        string pass = "";
        public string Pass { get { return pass; } set { pass = value; } }

        string passConf = "";
        public string PassConf { get { return passConf; } set { passConf = value; } }

        int codeConf = 0;
        public int CodeConf { get { return codeConf; } set { codeConf = value; } }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            WithoutLog = new Command(GoWithout);
            SendCode = new Command(async() => await SendCodeF());
            ChangeCommand = new Command(async () => await Change());
            GoChange = new Command(StartChange);
            EndChange = new Command(StopChange);
        }

        private async void OnLoginClicked()
        {
            Application.Current.MainPage = new AppShell();
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }

        private async void GoWithout()
        {
            Application.Current.MainPage = new AppShell();
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            (Shell.Current as AppShell).GetInDr().ForEach(a => a.IsEnabled = false);
        }

        public async Task<bool> Login(string data)
        {
            if (await Service.Log(data))
            {
                OnLoginClicked();
                return true;
            }
            return false;
        }

        void StartChange()
        {
            BgVisible = true;
            MainVisible = false;
            EmailVisible = true;
        }
        void StopChange()
        {
            BgVisible = false;
            MainVisible = true;
            EmailVisible = false;
            changeVisible = false;
        }


        public async Task SendCodeF()
        {
            Regex rx = new Regex(@"^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;]{0,1}\s*)+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            if (email.Length < 5 || !rx.IsMatch(Email))
            {
                Error = "To nie jest email";
                return;
            }

            if (await Service.SendConfAnon(email))
            {
                EmailVisible = false;
                ChangeVisible = true;
            }
            else
            {
                Error = "Błędny email";
            }
            return;
        }

        public async Task Change()
        {
            if (codeConf <= 0)
            {
                Error = "Błędny kod";
                return;
            }
            Regex rx = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{5,}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            if (!rx.IsMatch(pass))
            {
                Error = "Hasło powinno zawierać jedną literę, liczbę i znak specialny";
                return;
            }
            if (pass != passConf)
            {
                Error = "Hasła są różne";
                return;
            }


            ChangePass info = new ChangePass
            {
                Email = email,
                Password = pass,
                PasswordConfirm = passConf,
                EmailCode = codeConf
            };
            if (await Service.ChangePass(JsonSerializer.Serialize<ChangePass>(info)))
            {
                BgVisible = false;
                ChangeVisible = false;
                MainVisible = true;
                Error = "";
            }
            else
            {
                Error = "Błędne dane / Niepowodzenie";
            }
            return ;
        }
    }
}
