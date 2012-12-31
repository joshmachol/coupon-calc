using CouponCalc.Model;
using System;
using System.Collections.Generic;

namespace CouponCalc.Design
{
    public class DesignDataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to create design time data

            var item = new DataItem("Welcome to MVVM Light [design]");
            callback(item, null);
        }

        public void GetCarts(Action<IEnumerable<Cart>, Exception> callback)
        {
            var carts = new List<Cart>();

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

                carts.Add(cart);
            }

            callback(carts, null);
        }
    }
}