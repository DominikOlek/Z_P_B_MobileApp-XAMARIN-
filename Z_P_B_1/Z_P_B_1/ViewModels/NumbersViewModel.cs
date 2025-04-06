using Plugin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Z_P_B_1.ViewModels
{
    public class NumbersViewModel : BaseViewModel
    {
        public Command<string> PhoneCall { get; }
        public NumbersViewModel()
        {
            PhoneCall = new Command<string>(a => Call(a));
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }

        void Call(string number)
        {
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
                try
                {
                    phoneDialer.MakePhoneCall(number);
                }
                catch { }
        }
    }
}