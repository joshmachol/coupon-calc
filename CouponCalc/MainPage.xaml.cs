﻿using CouponCalc.Common;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using System;
using System.Windows.Navigation;

namespace CouponCalc
{
    public partial class MainPage : PhoneApplicationPage
    {
        private const int CartsPivotItemIndex = 0;

        public MainPage()
        {
            InitializeComponent();

            Messenger.Default.Register<Uri>(this, MessageTokens.NavigationRequest, uri => NavigationService.Navigate(uri));

            Loaded += (sender, args) =>
            {
                Pivot.SelectionChanged += (o, eventArgs) =>
                {
                    ApplicationBar.IsVisible = Pivot.SelectedIndex == CartsPivotItemIndex;
                };
            };
        }

        private void AddCart(object sender, EventArgs e)
        {
            App.Locator.Main.AddCart();
        }

        private void EditSelectedCart(object sender, EventArgs e)
        {
            if (App.Locator.Main.GoToCartDetails.CanExecute(App.Locator.Main.SelectedCart))
            {
                App.Locator.Main.GoToCartDetails.Execute(App.Locator.Main.SelectedCart);
            }
        }

        private void DeleteSelectedCart(object sender, EventArgs e)
        {
            App.Locator.Main.DeleteCart(App.Locator.Main.SelectedCart);
        }
    }
}