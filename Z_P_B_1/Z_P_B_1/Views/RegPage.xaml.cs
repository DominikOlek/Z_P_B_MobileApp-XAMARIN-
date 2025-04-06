using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Z_P_B_1.ViewModels;
using Xamarin.Essentials;
using System.Text.RegularExpressions;
using System.Text.Json;
using Z_P_B_1.Models;

namespace Z_P_B_1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegPage : ContentPage
    {
        bool Reg = true;
        RegViewModel _view;
        public RegPage()
        {
            InitializeComponent();
            this.BindingContext = _view = new RegViewModel();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Reg = true;
            var Imie = imie.Text;
            CheckFill(imie, "Imię", Imie);
            var Nazwisko = nazwisko.Text;
            CheckFill(nazwisko, "Nazwisko", Nazwisko);
            var Pesel = pesel.Text;
            CheckFill(pesel, "Pesel", Pesel,11);
            var Email = email.Text;
            if (CheckFill(email, "Email", Email, 5))
            {
                CheckExpression(@"^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;]{0,1}\s*)+$", Email, email, "To nie jest adres email");
            }
            var Haslo = haslo.Text;
            if (CheckFill(haslo, "Hasło", Haslo, 5))
            {
                CheckExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{5,}$", Haslo, haslo, "Ma zawierać literę,liczbę,znak specialny");
            }
            var HasloP = hasloP.Text;
            if(HasloP != Haslo)
            {
                hasloP.Text = null;
                hasloP.Placeholder = "Hasła nie są takie same";
                hasloP.PlaceholderColor = Color.FromRgb(190, 32, 32);
                Reg = false;
            }
            var DataUr = data.Date;
            var Tel = tel.Text;
            CheckFill(tel, "Telefon", Tel, 9);

            if (Reg)
            {
                Error.IsVisible = false;
                var data = new RegisterData
                {
                    pesel = Pesel,
                    imie = Imie,
                    nazwisko = Nazwisko,
                    password = Haslo,
                    confirmPassword = HasloP,
                    email = Email,
                    nr_tel = Tel,
                    data_ur = DataUr
                };
                if(!await _view.Register(JsonSerializer.Serialize(data)))
                {
                    Error.IsVisible = true;
                }
                //rejestracja
            }
            
        }
        private bool CheckFill(Entry field,string comunicate,string value,int len = 3)
        {
            if (value == null || value.Length < len)
            {
                field.Text = null;
                field.Placeholder = comunicate+ " jest za krótkie";
                field.PlaceholderColor = Color.FromRgb(190, 32, 32);
                Reg = false;
                return false;
            }
            return true;
        }
        private void CheckExpression(string regularExp,string value,Entry field,string comunicate)
        {
            Regex rx = new Regex(regularExp,RegexOptions.Compiled|RegexOptions.IgnoreCase);
            bool match = rx.IsMatch(value);
            if(!match)
            {
                field.Text = null;
                field.Placeholder = comunicate;
                field.PlaceholderColor = Color.FromRgb(190, 32, 32);
                Reg = false;
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                await Browser.OpenAsync("regulamin.html", BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex){}
        }
    }
}