using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Z_P_B_1.ViewModels
{
    public class InfoNumbersViewModel : BaseViewModel
    {
        public InfoNumbersViewModel()
        {

        }
        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}