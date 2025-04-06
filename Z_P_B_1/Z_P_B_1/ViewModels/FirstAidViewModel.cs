using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Z_P_B_1.Models;
using Z_P_B_1.Views;

namespace Z_P_B_1.ViewModels
{
    public class FirstAidViewModel : BaseViewModel
    {
        private FirstAidDto _selectedItem;
        public Command<FirstAidDto> ItemTappe { get; }
        public Command test { get; }
        public FirstAidViewModel()
        {
            ItemTappe = new Command<FirstAidDto>(a=>OnItemSelectedd(a));
        }
        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }
        public FirstAidDto SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelectedd(value);
            }
        }
        async void OnItemSelectedd(FirstAidDto item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(FirstAidItem)}?{nameof(FirstAidItemViewModel.ItemId)}={item.Id}&{nameof(FirstAidItemViewModel.ItemOther)}={item.IsOther}");
        }
    }
}