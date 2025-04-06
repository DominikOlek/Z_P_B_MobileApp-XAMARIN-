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
    public class DriverInfoViewModel : BaseViewModel
    {
        public IDataServiceOnlyNet<DriverInfoDto> DataStoree => DependencyService.Get<IDataServiceOnlyNet<DriverInfoDto>>();
        bool page1;
        bool page2;
        public bool Page1 { get { return page1; } set { SetProperty(ref page1, value); } }
        public bool Page2 { get { return page2; } set { SetProperty(ref page2, value); } }
        public Command<string> ChangeVieCommand { get; }
        public ObservableCollection<MandatDto> Mandats { get; set; }
        public ObservableCollection<DriverPrev> Hist { get; }
        DateTime lasReqTime = DateTime.MinValue;

        public Command GetInfo { get; }
        public DriverInfoViewModel()
        {
            Page1 = true;
            Page2 = false;
            ChangeVieCommand = new Command<string>(obj => ChangeView(obj));
            GetInfo = new Command(async (a) => await GetElements());
            Mandats = new ObservableCollection<MandatDto>();
            Hist = new ObservableCollection<DriverPrev>();
        }
        public void ChangeView(string numbesr)
        {
            int number = int.Parse(numbesr);
            if (number == 1) { Page1 = true; Page2 = false; }
            if (number == 2) { Page1 = false; Page2 = true; }
            
        }

        public async Task GetElements()
        {
            if (lasReqTime.AddSeconds(15) <= DateTime.Now)
            {
                lasReqTime = DateTime.Now;
                IsBusy = true;
                try
                {
                    var item = await DataStoree.GetItems();
                    if(Mandats.Count > 0) Mandats.Clear();
                    foreach (MandatDto mand in item.mandats)
                    {
                        Mandats.Add(mand);
                    }
                    if (Hist.Count > 0) Hist.Clear();
                    for(int i =0; i<item.history.Length -1;i++)
                    {
                        Hist.Add(new DriverPrev { Describe= item.history[i]} );
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