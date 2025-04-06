using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Z_P_B_1.ViewModels;
using Z_P_B_1.Models.HardData;

namespace Z_P_B_1.Views
{
    public partial class NumbersPage: ContentPage
    {
        NumbersViewModel _viewModel = new NumbersViewModel();
        public NumbersPage()
        {
            InitializeComponent();
            _viewModel.OnAppearing();
            ItemsListView.ItemsSource = AllNumbersData.DataList;
            BindingContext = _viewModel = new NumbersViewModel();
        }

    }
}