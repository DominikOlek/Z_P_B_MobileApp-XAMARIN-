using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Z_P_B_1.ViewModels;

namespace Z_P_B_1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        SettingsViewModel _viewModel;
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new SettingsViewModel();
            GetData();
            _viewModel.GetEl(main, code, code2);
            _viewModel.OnAppearing();
        }
        private async void GetData()
        {
            await _viewModel.GetData();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            var b = (Button)sender;
            if (b.Text == "Ciemny")
            {
                App.Current.Resources["textColor"] = Color.Black;
                App.Current.Resources["backgroundColorDarker"] = Color.White;
                App.Current.Resources["backgroundColor"] = "#CFCFCF";
                b.Text = "Jasny";
            }
            else
            {
                App.Current.Resources["textColor"] = Color.White;
                App.Current.Resources["backgroundColorDarker"] = Color.Black;
                App.Current.Resources["backgroundColor"] = "#303030";
                App.Current.Resources["textColor"] = Color.White;
                b.Text = "Ciemny";
            }
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            var b = (Button)sender;
            if (b.Text == "Normalny")
            {
                App.Current.Resources["textSize"] = 20;
                App.Current.Resources["textSizeBigger"] = 25;
                App.Current.Resources["textSizeBigg"] = 30;
                b.Text = "Duży";
            }
            else
            {
                App.Current.Resources["textSize"] = 16;
                App.Current.Resources["textSizeBigger"] = 20;
                App.Current.Resources["textSizeBigg"] = 25;
                b.Text = "Normalny";
            }
        }
    }
}