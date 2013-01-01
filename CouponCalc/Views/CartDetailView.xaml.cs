using CouponCalc.Model;
using Microsoft.Phone.Controls;
using System;
using System.Windows.Navigation;

namespace CouponCalc.Views
{
    public partial class CartDetailView : PhoneApplicationPage
    {
        public const string NavigateToMeUri = "/Views/CartDetailView.xaml?id=";

        private Cart _cart;

        public CartDetailView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (DataContext != null) return;
            var id = NavigationContext.QueryString["id"];
            _cart = App.Locator.Main.GetCart(new Guid(id));
            DataContext = _cart;
        }

        /// <summary>
        /// Gets the navigation URI for this view.
        /// </summary>
        /// <param name="cart">The cart.</param>
        /// <returns></returns>
        public static Uri GetNavigationUri(Cart cart)
        {
            return new Uri(CartDetailView.NavigateToMeUri + cart.Id, UriKind.Relative);
        }
    }
}