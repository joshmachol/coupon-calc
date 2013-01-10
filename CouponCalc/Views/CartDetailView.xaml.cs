using System.Linq;
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
            App.Locator.Main.SelectedItem = _cart.Items.FirstOrDefault();
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

        private void AddItem(object sender, EventArgs e)
        {
            App.Locator.Main.AddItemToCart(_cart);
        }

        private void EditSelectedItem(object sender, EventArgs e)
        {
            if (App.Locator.Main.GoToItemDetails.CanExecute(App.Locator.Main.SelectedItem))
            {
                App.Locator.Main.GoToItemDetails.Execute(App.Locator.Main.SelectedItem);
            }
        }

        private void DeleteSelectedItem(object sender, EventArgs e)
        {
            App.Locator.Main.DeleteItem(App.Locator.Main.SelectedItem);
        }
    }
}