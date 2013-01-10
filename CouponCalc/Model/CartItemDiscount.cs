using System;

namespace CouponCalc.Model
{
    public enum DiscountType
    {
        Store,
        Manufacturer,
    }

    public class CartItemDiscount : BindableBase
    {
        // Properties
        private CartItem _OwningItem;
        public CartItem OwningItem
        {
            get { return _OwningItem; }
            set { SetProperty(ref _OwningItem, value, "OwningItem"); }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { SetProperty(ref _Name, value, "Name"); }
        }

        private DiscountType _Type;
        public DiscountType Type
        {
            get { return _Type; }
            set { SetProperty(ref _Type, value, "Type"); }
        }

        private double _Discount;
        public double Discount
        {
            get { return _Discount; }
            set { SetProperty(ref _Discount, value, "Discount"); }
        }

        private DateTime _ExpirationDate;
        public DateTime ExpirationDate
        {
            get { return _ExpirationDate; }
            set { SetProperty(ref _ExpirationDate, value, "ExpirationDate"); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CartItemDiscount" /> class.
        /// </summary>
        /// <param name="owningItem">The owning item.</param>
        public CartItemDiscount(CartItem owningItem)
        {
            OwningItem = owningItem;
        }
    }
}