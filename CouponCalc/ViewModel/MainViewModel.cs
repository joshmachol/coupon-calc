using CouponCalc.Common;
using CouponCalc.Model;
using CouponCalc.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace CouponCalc.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        private RelayCommand<Cart> _GoToCartDetails;
        public RelayCommand<Cart> GoToCartDetails
        {
            get
            {
                return _GoToCartDetails ?? (_GoToCartDetails =
                    new RelayCommand<Cart>(cart =>
                        SendNavigationRequestMessage(CartDetailView.GetNavigationUri(cart))
                    ));
            }
        }

        private RelayCommand<CartItem> _GoToItemDetails;
        public RelayCommand<CartItem> GoToItemDetails
        {
            get
            {
                return _GoToItemDetails ?? (_GoToItemDetails =
                    new RelayCommand<CartItem>(item =>
                        SendNavigationRequestMessage(ItemDetailView.GetNavigationUri(item))
                    ));
            }
        }

        #region Inpc Prop Carts

        /// <summary>
        /// The <see cref="Carts" /> property's name.
        /// </summary>
        public const string CartsPropertyName = "Carts";

        private readonly ObservableCollection<Cart> _Carts = new ObservableCollection<Cart>();

        /// <summary>
        /// Sets and gets the Carts property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<Cart> Carts
        {
            get { return _Carts; }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _dataService.GetCarts((carts, error) =>
            {
                if (error != null)
                {
                    // Report error here
                    return;
                }

                foreach (var cart in carts)
                {
                    Carts.Add(cart);
                }
            });
        }

        /// <summary>
        /// Sends a navigation request message.
        /// </summary>
        /// <param name="uri">The URI.</param>
        public void SendNavigationRequestMessage(Uri uri)
        {
            MessengerInstance.Send(uri, MessageTokens.NavigationRequest);
        }

        /// <summary>
        /// Gets the cart with the given Id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public Cart GetCart(Guid id)
        {
            return Carts.FirstOrDefault(c => c.Id.Equals(id));
        }

        /// <summary>
        /// Gets the item with the given Id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public CartItem GetItem(Guid id)
        {
            return Carts.SelectMany(c => c.Items).FirstOrDefault(i => i.Id.Equals(id));
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}