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
    public partial class HealthPage : ContentPage
    {
        HealthPageViewModel _viewModel = new HealthPageViewModel();
        public HealthPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new HealthPageViewModel();
            GetData();
        }

        private async void GetData()
        {
            await _viewModel.GetElements();
        }
    }
}