using System.ComponentModel;
using Xamarin.Forms;
using Z_P_B_1.ViewModels;

namespace Z_P_B_1.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}