using CouponCalc.Common;
using CouponCalc.Model;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using System;
using System.Linq;

namespace CouponCalc.Views
{
    public partial class ItemDetailView : PhoneApplicationPage
    {
        private const int DiscountsPivotItemIndex = 1;
        public const string NavigateToMeUri = "/Views/ItemDetailView.xaml?id=";

        private CartItem _item;

        public ItemDetailView()
        {
            InitializeComponent();

            Messenger.Default.Register<Uri>(this, MessageTokens.NavigationRequest, uri => NavigationService.Navigate(uri));

            Loaded += (sender, args) =>
            {
                // Pivot requires ListBox.SelectedItem binding changes to happen here
                if (_item != null)
                {
                    App.Locator.Main.SelectedDiscount = _item.Discounts.FirstOrDefault();
                }

                Pivot.SelectionChanged += (o, eventArgs) =>
                {
                    ApplicationBar.IsVisible = Pivot.SelectedIndex == DiscountsPivotItemIndex;
                };
            };
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var id = NavigationContext.QueryString["id"];
            _item = App.Locator.Main.GetItem(new Guid(id));
            DataContext = _item;
        }

        public static Uri GetNavigationUri(CartItem item)
        {
            return new Uri(ItemDetailView.NavigateToMeUri + item.Id, UriKind.Relative);
        }

        private void AddDiscount(object sender, EventArgs e)
        {
            App.Locator.Main.AddDiscountToItem(_item);
        }

        private void EditSelectedDiscount(object sender, EventArgs e)
        {
            var discount = App.Locator.Main.SelectedDiscount;
            if (App.Locator.Main.GoToDiscountDetails.CanExecute(discount))
            {
                App.Locator.Main.GoToDiscountDetails.Execute(discount);
            }
        }

        private void DeleteSelectedDiscount(object sender, EventArgs e)
        {
            App.Locator.Main.DeleteDiscount(App.Locator.Main.SelectedDiscount);
        }
    }
}