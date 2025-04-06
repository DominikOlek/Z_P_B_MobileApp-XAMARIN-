using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Z_P_B_1.Models;
using Z_P_B_1.Services;

namespace Z_P_B_1.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private bool _reff;
        public IAccountService Service => DependencyService.Get<IAccountService>();
        public ObservableCollection<Info> infoColection { get; }
        public Command LoadItemsCommand { get; }
        public MainPageViewModel()
        {
            infoColection = new ObservableCollection<Info>();
            LoadItemsCommand = new Command(async (a) => await LoadItemId());
        }
        public void OnAppearing()
        {
            IsBusy = false;
            LoadItemId();
        }


        public async Task LoadItemId()
        {
            try
            {
                var w = await Service.GetInfo(false);
                if (w != null)
                {
                    if (w.info == "") w.info = "Brak powiadomień";
                    infoColection.Clear();
                    infoColection.Add(w);
                }
                else 
                {
                    infoColection.Clear();
                    infoColection.Add(new Info { name = "anonimowy", lastName = "użytkowniku", info = "Zaloguj się by korzstać z pełni" });
                }
                IsBusy = false;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}