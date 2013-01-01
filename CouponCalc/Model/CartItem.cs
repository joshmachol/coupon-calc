using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace CouponCalc.Model
{
    public class CartItem : BindableBase
    {
        // Properties
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { SetProperty(ref _Name, value, "Name"); }
        }

        private double _Price;
        public double Price
        {
            get { return _Price; }
            set { SetProperty(ref _Price, value, "Price"); }
        }

        private bool _Taxable;
        public bool Taxable
        {
            get { return _Taxable; }
            set { SetProperty(ref _Taxable, value, "Taxable"); }
        }

        private int _Quantity;
        public int Quantity
        {
            get { return _Quantity; }
            set { SetProperty(ref _Quantity, value, "Quantity"); }
        }

        private DateTime _ExpirationDate;
        public DateTime ExpirationDate
        {
            get { return _ExpirationDate; }
            set { SetProperty(ref _ExpirationDate, value, "ExpirationDate"); }
        }

        private ObservableCollection<CartItemDiscount> _Discounts;
        public ObservableCollection<CartItemDiscount> Discounts
        {
            get { return _Discounts ?? (_Discounts = new ObservableCollection<CartItemDiscount>()); }
            set { SetProperty(ref _Discounts, value, "Discounts"); }
        }

        // Indexers
        public CartItemDiscount this[int index]
        {
            get { return Discounts[index]; }
            set { Discounts[index] = value; }
        }

        public CartItemDiscount this[Guid id]
        {
            get { return GetDiscount(id); }
        }

        // Methods
        public CartItemDiscount GetDiscount(Guid id)
        {
            return Discounts.FirstOrDefault(d => d.Id.Equals(id));
        }
    }
}