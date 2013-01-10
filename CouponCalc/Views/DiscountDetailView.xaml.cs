using CouponCalc.Model;
using Microsoft.Phone.Controls;
using System;

namespace CouponCalc.Views
{
    public partial class DiscountDetailView : PhoneApplicationPage
    {
        public const string NavigateToMeUri = "/Views/DiscountDetailView.xaml?id=";
        private CartItemDiscount _discount;

        public DiscountDetailView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var id = NavigationContext.QueryString["id"];
            _discount = App.Locator.Main.GetDiscount(new Guid(id));
            DataContext = _discount;
        }

        internal static Uri GetNavigationUri(Model.CartItemDiscount discount)
        {
            return new Uri(NavigateToMeUri + discount.Id, UriKind.Relative);
        }
    }
}