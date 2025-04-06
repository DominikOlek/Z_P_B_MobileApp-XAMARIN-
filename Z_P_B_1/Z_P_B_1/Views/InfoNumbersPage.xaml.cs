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
    public partial class InfoNumbersPage : ContentPage
    {
        InfoNumbersViewModel _viewModel = new InfoNumbersViewModel();
        public InfoNumbersPage()
        {
            InitializeComponent();
            _viewModel.OnAppearing();
        }
    }
}