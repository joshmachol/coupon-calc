using CouponCalc.Data;
using Microsoft.Phone.Controls;
using System.Collections.ObjectModel;
using System.Windows;

namespace CouponCalc
{
    public partial class MainPage : PhoneApplicationPage
    {
        private ObservableCollection<Cart> _Carts;
        public ObservableCollection<Cart> Carts
        {
            get { return _Carts ?? (_Carts = new ObservableCollection<Cart>()); }
        }

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            DataContext = Carts;

            Loaded += LoadCarts;
        }

        private void LoadCarts(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                var cart = new Cart();
                cart.Name = "Cart " + (i + 1);
                cart.Store = Store.Publix;

                for (int j = 0; j < 10; j++)
                {
                    var item = new CartItem();
                    item.Name = "Cart Item " + (j + 1);
                    item.Price = j * 10;
                    item.Quantity = j;
                    item.Taxable = i % 2 == 0;

                    for (int k = 0; k < 3; k++)
                    {
                        var discount = new CartItemDiscount();
                        discount.Name = "Discount " + (i + 1);
                        discount.Discount = k * .75;
                        discount.Type = DiscountType.Store;
                        item.Discounts.Add(discount);
                    }

                    cart.Items.Add(item);
                }

                Carts.Add(cart);
            }
        }
    }
}