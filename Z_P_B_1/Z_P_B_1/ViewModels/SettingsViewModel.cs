using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Z_P_B_1.Models;
using Z_P_B_1.Services;

namespace Z_P_B_1.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public IAccountService Service => DependencyService.Get<IAccountService>();
        public ObservableCollection<Info> InfoColection { get; }
        public Command SendCode { get; }
        public Command GoChange { get; }
        public int Code { get; set; }


        VisualElement mainE;
        VisualElement codeBg;
        VisualElement codeGrid;

        string error = string.Empty;
        public string Error
        {
            get { return error; }
            set { SetProperty(ref error, value); }
        }

        public SettingsViewModel()
        {
            Title = "Ustawienia";
            InfoColection = new ObservableCollection<Info>();
            SendCode = new Command(obj => Send());
            GoChange = new Command(obj => Change());
            InfoColection.Add(new Info { });
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }
        public void GetEl(VisualElement a,VisualElement b, VisualElement c)
        {
            mainE = a;
            codeBg = b;
            codeGrid = c;
            Device.BeginInvokeOnMainThread(() =>
            {
                mainE.IsVisible = true;
                codeGrid.IsVisible = false;
                codeBg.IsVisible = false;
            });
        }

        public async void Send()
        {
            var first = InfoColection.First();
            if (!CheckFill(first.name, "Wpisz imię") || !CheckFill(first.lastName, "Wpisz lastName") || !CheckFill(first.nr_tel, "Wpisz nr telefonu") || !CheckFill(first.pesel, "Wpisz pesel"))
                return;

            Regex rx = new Regex(@"^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;]{0,1}\s*)+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            if (first.email.Length < 5 || !rx.IsMatch(first.email))
            {
                Error = "To nie jest email";
                return;
            }
            rx = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{5,}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            if (first.password != null && !rx.IsMatch(first.password))
            {
                Error = "Hasło powinno zawierać jedną literę, liczbę i znak specialny";
                return;
            }
            if (first.password != null && first.password != first.confirmPassword)
            {
                Error = "Hasła są różne";
                return;
            }
            if (DateTime.Compare(first.nextChange,DateTime.Today) <= 0)
            {
                if(await Service.SendConf())
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        mainE.IsVisible = false;
                        codeGrid.IsVisible = true;
                        codeBg.IsVisible = true;
                    });
                }
            }
        }

        public async Task GetData()
        {
            var w = await Service.GetInfo(true);
            if (w != null)
            {
                InfoColection.Clear();
                InfoColection.Add(w);
            }
        }


        public async void Change()
        {
            var first = InfoColection.First();
            first.code = Code;
            if (first.code <= 0)
            {
                Error = "Błędny kod";
                return;
            }

            if (await Service.ChangeInfo(JsonSerializer.Serialize<Info>(first)))
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    mainE.IsVisible = true;
                    codeGrid.IsVisible = false;
                    codeBg.IsVisible = false;
                });
                Error = "";
                GetData();
            }
            else
            {

                Error = "Błędne dane";
            }
        }

        private bool CheckFill(string value, string comunicate, int len = 3)
        {
            if (value == null || value.Length < len)
            {
                Error = comunicate;
                return false;
            }
            return true;
        }
    }
}