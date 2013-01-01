using CouponCalc.Common;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using System;
using System.Windows.Navigation;

namespace CouponCalc
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();

            Messenger.Default.Register<Uri>(this, MessageTokens.NavigationRequest, uri => NavigationService.Navigate(uri));
        }
    }
}