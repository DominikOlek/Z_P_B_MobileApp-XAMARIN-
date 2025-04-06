using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Z_P_B_1.Models.HardData;
using Z_P_B_1.ViewModels;

namespace Z_P_B_1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OtherBehaviorPage : ContentPage
    {
        FirstAidViewModel _viewModel = new FirstAidViewModel();
        public OtherBehaviorPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new FirstAidViewModel();
            ItemsListView.ItemsSource = FirstAidData.DataList.Where(a => a.IsOther == true);
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var SearchB = (SearchBar)sender;
            ItemsListView.ItemsSource = FirstAidData.DataList.Where(a => a.Title.ToLower().Contains(SearchB.Text.ToLower()) && a.IsOther == true);
        }
    }
}