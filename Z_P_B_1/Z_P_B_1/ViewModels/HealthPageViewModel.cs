using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Z_P_B_1.Views;

using Xamarin.Forms;
using Z_P_B_1.Services.Interfaces;
using Z_P_B_1.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Z_P_B_1.ViewModels
{
    public class HealthPageViewModel : BaseViewModel
    {
        public IDataServiceOnlyNet<List<RecipeDto>> DataStoree => DependencyService.Get<IDataServiceOnlyNet<List<RecipeDto>>>();
        public ObservableCollection<RecipeDto> Recipes { get; }
        DateTime lasReqTime = DateTime.MinValue;

        public Command GetInfo { get; }
        public HealthPageViewModel()
        {
            GetInfo = new Command(async (a) => await GetElements());
            Recipes = new ObservableCollection<RecipeDto>();
        }

        public async Task GetElements()
        {
            if (lasReqTime.AddSeconds(15) <= DateTime.Now)
            {
                lasReqTime = DateTime.Now;
                IsBusy = true;
                try
                {
                    List<RecipeDto> item = await DataStoree.GetItems();
                    if (Recipes.Count > 0) Recipes.Clear();
                    foreach (RecipeDto rec in item)
                    {
                        Recipes.Add(rec);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}