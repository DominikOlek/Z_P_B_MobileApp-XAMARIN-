using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Z_P_B_1.ViewModels;
using System.Text.RegularExpressions;
using Z_P_B_1.Models;
using System.Text.Json;

namespace Z_P_B_1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        bool Log = true;
        public LoginViewModel _view;
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = _view = new LoginViewModel();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            // Application.Current.MainPage = new AppShell();
            await Navigation.PushAsync(new RegPage());
        }

        private bool CheckFill(Entry field, string comunicate, string value, int len = 3)
        {
            if (value == null || value.Length < len)
            {
                field.Text = null;
                field.Placeholder = comunicate + " jest za krótkie";
                field.PlaceholderColor = Color.FromRgb(190, 32, 32);
                Log = false;
                return false;
            }
            return true;
        }

        private async void LoginClicked(object sender, EventArgs e)
        {
            Log = true;
            var Email = email.Text;
            if (CheckFill(email, "Email", Email, 5))
            {
                Regex rx = new Regex(@"^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;]{0,1}\s*)+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                bool match = rx.IsMatch(Email);
                if (!match)
                {
                    email.Text = null;
                    email.Placeholder = "Adres nie jest w poprawnym formacie";
                    email.PlaceholderColor = Color.FromRgb(190, 32, 32);
                    Log = false;
                }
            }
            var Haslo = haslo.Text;
            CheckFill(haslo, "Hasło", Haslo, 5);

            if (Log)
            {
                Error.IsVisible = false;
                var data = new LoginDto
                {
                    password = Haslo,
                    email = Email,

                };
                if (!await _view.Login(JsonSerializer.Serialize(data)))
                {
                    Error.IsVisible = true;
                }
                //logowanie
            }
        }

    }
}