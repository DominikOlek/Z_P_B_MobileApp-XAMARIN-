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
using Z_P_B_1.Services.Interfaces;

namespace Z_P_B_1.ViewModels
{
    public class TaryfikatorsPageViewModel : BaseViewModel
    {
        public IDataMainServices<Taryfikator> DataStoree => DependencyService.Get<IDataMainServices<Taryfikator>>();
        public Command<string> LoadItemsCommand { get; }
        public Command<object> SearcherItems { get; }
        public ObservableCollection<Taryfikator> taryfikators { get; }
        DateTime lasReqTime = DateTime.MinValue;
        public TaryfikatorsPageViewModel()
        {
            Title = "Taryfikatory";
            taryfikators = new ObservableCollection<Taryfikator>();
            LoadItemsCommand = new Command<string>(async (a) => await ExecuteLoadItemsCommand(a));
            SearcherItems = new Command<object>(obj => Searcher(obj));
        }
        public async Task ExecuteLoadItemsCommand(string a)
        {
            if (lasReqTime.AddMinutes(3) <= DateTime.Now)
            {
                lasReqTime = DateTime.Now;
                IsBusy = true;
                try
                {
                    AddToList(await DataStoree.GetItems(a == "1"));
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

        public void Searcher(object sender)
        {
            SearchBar sr = (SearchBar)sender;
            AddToList(DataStoree.Search(sr.Text));
        }
        public void AddToList(IEnumerable<Taryfikator> items)
        {
            if (items != null)
            {
                taryfikators.Clear();
                foreach (var item in items)
                {
                    taryfikators.Add(item);
                }
            }
        }

        public void OnAppearing()
        {
            lasReqTime = DateTime.MinValue;
            IsBusy = true;
        }
    }
}