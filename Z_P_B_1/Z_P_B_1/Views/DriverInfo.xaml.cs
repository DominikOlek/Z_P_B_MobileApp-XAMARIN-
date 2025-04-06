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
    public partial class DriverInfo : ContentPage
    {
        DriverInfoViewModel _viewModel = new DriverInfoViewModel();
        public bool Page1 = true;
        public DriverInfo()
        {
            InitializeComponent();
            _viewModel.OnAppearing();
            BindingContext = _viewModel = new DriverInfoViewModel();
            Resources["label1Style"] = Resources["active"];
            Resources["label2Style"] = Resources["unActive"];
            GetData();
        }

        private async void GetData()
        {
            await _viewModel.GetElements();
        }
        public void SetActive()
        {

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (_viewModel.Page1)
            {
                Resources["label1Style"] = Resources["active"];
                Resources["label2Style"] = Resources["unActive"];
            }
            else
            {
                Resources["label1Style"] = Resources["unActive"];
                Resources["label2Style"] = Resources["active"];
            }
        }   
    }
}