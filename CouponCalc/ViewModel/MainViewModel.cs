using CouponCalc.Common;
using CouponCalc.Model;
using CouponCalc.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using WindowsPhoneHelp;

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

        private RelayCommand<Store> _GoToCartsByStoreDetails;
        public RelayCommand<Store> GoToCartsByStoreDetails
        {
            get
            {
                return _GoToCartsByStoreDetails ?? (_GoToCartsByStoreDetails =
                    new RelayCommand<Store>(store =>
                        SendNavigationRequestMessage(CartsByStoreDetailView.GetNavigationUri(store))
                    ));
            }
        }

        private RelayCommand<CartItemDiscount> _GoToDiscountDetails;
        public RelayCommand<CartItemDiscount> GoToDiscountDetails
        {
            get
            {
                return _GoToDiscountDetails ?? (_GoToDiscountDetails =
                    new RelayCommand<CartItemDiscount>(discount =>
                        SendNavigationRequestMessage(DiscountDetailView.GetNavigationUri(discount))
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

        #region Inpc Prop SelectedCart

        /// <summary>
        /// The <see cref="SelectedCart" /> property's name.
        /// </summary>
        public const string SelectedCartPropertyName = "SelectedCart";

        private Cart _SelectedCart;

        /// <summary>
        /// Sets and gets the SelectedCart property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Cart SelectedCart
        {
            get { return _SelectedCart; }

            set
            {
                if (_SelectedCart == value)
                {
                    return;
                }

                _SelectedCart = value;
                RaisePropertyChanged(SelectedCartPropertyName);
            }
        }

        #endregion

        #region Inpc Prop SelectedItem

        /// <summary>
        /// The <see cref="SelectedItem" /> property's name.
        /// </summary>
        public const string SelectedItemPropertyName = "SelectedItem";

        private CartItem _SelectedItem;

        /// <summary>
        /// Sets and gets the SelectedItem property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public CartItem SelectedItem
        {
            get { return _SelectedItem; }

            set
            {
                if (_SelectedItem == value)
                {
                    return;
                }

                _SelectedItem = value;
                RaisePropertyChanged(SelectedItemPropertyName);
            }
        }

        #endregion

        #region Inpc Prop SelectedDiscount

        /// <summary>
        /// The <see cref="SelectedDiscount" /> property's name.
        /// </summary>
        public const string SelectedDiscountPropertyName = "SelectedDiscount";

        private CartItemDiscount _SelectedDiscount;

        /// <summary>
        /// Sets and gets the SelectedDiscount property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public CartItemDiscount SelectedDiscount
        {
            get { return _SelectedDiscount; }

            set
            {
                if (_SelectedDiscount == value)
                {
                    return;
                }

                _SelectedDiscount = value;
                RaisePropertyChanged(SelectedDiscountPropertyName);
            }
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

                SelectedCart = Carts.FirstOrDefault();
                if (SelectedCart != null) SelectedItem = SelectedCart.Items.FirstOrDefault();
                if (SelectedItem != null) SelectedDiscount = SelectedItem.Discounts.FirstOrDefault();
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

        /// <summary>
        /// Gets the discount with the given Id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public CartItemDiscount GetDiscount(Guid id)
        {
            return Carts.SelectMany(c => c.Items).SelectMany(i => i.Discounts).FirstOrDefault(d => d.Id.Equals(id));
        }

        /// <summary>
        /// Adds a new cart to the list of Carts and selects it.
        /// </summary>
        /// <returns>A new cart.</returns>
        public Cart AddCart()
        {
            var cart = new Cart();
            cart.Name = "New Cart";
            Carts.Add(cart);
            SelectedCart = cart;
            return cart;
        }

        /// <summary>
        /// Adds a new item to the given cart and selects it.
        /// </summary>
        /// <param name="cart">The cart.</param>
        /// <returns>A new item.</returns>
        public CartItem AddItemToCart(Cart cart)
        {
            Guard.Null(cart, "cart");
            var item = new CartItem(cart);
            item.Name = "New Item";
            cart.Items.Add(item);
            SelectedItem = item;
            return item;
        }

        /// <summary>
        /// Adds a new discount to the given item and selects it.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>A new discount.</returns>
        public CartItemDiscount AddDiscountToItem(CartItem item)
        {
            Guard.Null(item, "item");
            var discount = new CartItemDiscount(item);
            discount.Name = "New Discount";
            item.Discounts.Add(discount);
            SelectedDiscount = discount;
            return discount;
        }

        /// <summary>
        /// Deletes the cart.
        /// </summary>
        /// <param name="cart">The cart.</param>
        public void DeleteCart(Cart cart)
        {
            if (cart == null)
                return;

            Carts.Remove(cart);
        }

        /// <summary>
        /// Deletes the item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void DeleteItem(CartItem item)
        {
            if (item == null)
                return;

            item.OwningCart.Items.Remove(item);
        }

        /// <summary>
        /// Deletes the discount.
        /// </summary>
        public void DeleteDiscount(CartItemDiscount discount)
        {
            if (discount == null)
                return;

            discount.OwningItem.Discounts.Remove(discount);
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}