using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Z_P_B_1.Models.HardData;
using System.Diagnostics;

namespace Z_P_B_1.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    [QueryProperty(nameof(ItemOther), nameof(ItemOther))]
    public class FirstAidItemViewModel : BaseViewModel
    {
        private string itemId;
        private bool itemOther;
        private string titlee;
        private string steps;
        private string otherInfo;
        private string image;

        public string Titlee
        {
            get => titlee;
            set => SetProperty(ref titlee, value);
        }

        public string Steps
        {
            get => steps;
            set => SetProperty(ref steps, value);
        }

        public string OtherInfo
        {
            get => otherInfo;
            set => SetProperty(ref otherInfo, value);
        }

        public string Image
        {
            get => image;
            set => SetProperty(ref image, value);
        }


        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value,false);
            }
        }
        public bool ItemOther
        {
            get
            {
                return itemOther;
            }
            set
            {
                itemOther = value;
                LoadItemId(itemId,value);
            }
        }

        public void LoadItemId(string itemId,bool isOther)
        {
            try
            {
                var item = FirstAidData.DataList.Where(a=>a.Id.ToString() == itemId && a.IsOther == isOther);
                Titlee = item.First().Title;
                Steps = item.First().Steps;
                OtherInfo = item.First().OtherInforamtion;
                Image = item.First().TitlePhotoName;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
