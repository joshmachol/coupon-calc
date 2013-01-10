using CouponCalc.Common;
using CouponCalc.Model;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using System;
using System.Linq;
using System.Windows.Navigation;

namespace CouponCalc.Views
{
    public partial class CartsByStoreDetailView : PhoneApplicationPage
    {
        public const string NavigateToMeUri = "/Views/CartsByStoreDetailView.xaml?store=";

        private Store _store;

        public CartsByStoreDetailView()
        {
            InitializeComponent();

            Messenger.Default.Register<Uri>(this, MessageTokens.NavigationRequest, uri => NavigationService.Navigate(uri));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (DataContext != null) return;
            _store = (Store)Enum.Parse(typeof(Store), NavigationContext.QueryString["store"]);
            dynamic context = new
            {
                Store = _store,
                Carts = App.Locator.Main.Carts.Where(c => c.Store == _store)
            };
            DataContext = context;
        }

        public static Uri GetNavigationUri(Store store)
        {
            return new Uri(NavigateToMeUri + store, UriKind.Relative);
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