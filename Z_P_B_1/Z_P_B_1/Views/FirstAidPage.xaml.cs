using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Z_P_B_1.ViewModels;
using Z_P_B_1.Models;
using Z_P_B_1.Models.HardData;

namespace Z_P_B_1.Views
{
    public partial class FirstAidPage : ContentPage
    {
        FirstAidViewModel _viewModel;
        public FirstAidPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new FirstAidViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
            ItemsListView.ItemsSource = FirstAidData.DataList.Where(a => a.IsOther == false);
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var SearchB = (SearchBar)sender;
            ItemsListView.ItemsSource = FirstAidData.DataList.Where(a=>a.Title.ToLower().Contains(SearchB.Text.ToLower()) && a.IsOther == false);
        }

    }
}