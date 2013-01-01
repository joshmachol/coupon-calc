using CouponCalc.Common;
using CouponCalc.Model;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using System;

namespace CouponCalc.Views
{
    public partial class ItemDetailView : PhoneApplicationPage
    {
        public const string NavigateToMeUri = "/Views/ItemDetailView.xaml?id=";

        private CartItem _item;

        public ItemDetailView()
        {
            InitializeComponent();

            Messenger.Default.Register<Uri>(this, MessageTokens.NavigationRequest, uri => NavigationService.Navigate(uri));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var id = NavigationContext.QueryString["id"];
            _item = App.Locator.Main.GetItem(new Guid(id));
            DataContext = _item;
        }

        /// <summary>
        /// Gets the navigation URI for this view.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static Uri GetNavigationUri(CartItem item)
        {
            return new Uri(ItemDetailView.NavigateToMeUri + item.Id, UriKind.Relative);
        }
    }
}