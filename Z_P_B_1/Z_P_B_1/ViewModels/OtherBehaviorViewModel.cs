using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Z_P_B_1.ViewModels
{
    public class OtherBehaviorViewModel : BaseViewModel
    {
        public OtherBehaviorViewModel()
        {

        }
        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}