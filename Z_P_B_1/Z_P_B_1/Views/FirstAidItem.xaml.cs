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
    public partial class FirstAidItem : ContentPage
    {
        public FirstAidItem()
        {
            InitializeComponent();
            BindingContext = new FirstAidItemViewModel();
        }
    }
}