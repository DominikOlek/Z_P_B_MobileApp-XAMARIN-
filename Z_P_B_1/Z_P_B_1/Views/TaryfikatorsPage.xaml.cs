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
    public partial class TaryfikatorsPage : ContentPage
    {
        TaryfikatorsPageViewModel _viewModel ;
        public TaryfikatorsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new TaryfikatorsPageViewModel();
            GetData();
            _viewModel.OnAppearing();
        }

        private async void GetData()
        {
            await _viewModel.ExecuteLoadItemsCommand("0");
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.Searcher(sender);
        }
    }
}