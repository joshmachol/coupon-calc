using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace CouponCalc.Model
{
    public enum Store
    {
        Publix = 0,
        Target = 1,
        WalMart = 2,
        Kmart = 3,
        Kroger = 4,
    }

    public class Cart : BindableBase
    {
        // Properties
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { SetProperty(ref _Name, value, "Name"); }
        }

        private Store _Store;
        public Store Store
        {
            get { return _Store; }
            set { SetProperty(ref _Store, value, "Store"); }
        }

        private DateTime _ExpirationDate = DateTime.Now;
        public DateTime ExpirationDate
        {
            get { return _ExpirationDate; }
            set { SetProperty(ref _ExpirationDate, value, "ExpirationDate"); }
        }

        private ObservableCollection<CartItem> _Items;
        public ObservableCollection<CartItem> Items
        {
            get { return _Items ?? (Items = new ObservableCollection<CartItem>()); }
            set
            {
                SetProperty(ref _Items, value, "Items");

                if (_Items != null)
                {
                    _Items.CollectionChanged += (sender, args) =>
                        {
                            TotalBeforeDiscount = _Items.Sum(i => i.Price);
                            DiscountTotal = _Items.Sum(i => i.Discounts.Sum(d => d.Discount));
                            TotalAfterDiscount = _TotalBeforeDiscount - _DiscountTotal;
                        };
                }
            }
        }

        private double _TotalBeforeDiscount;
        public double TotalBeforeDiscount
        {
            get { return _TotalBeforeDiscount; }
            set { SetProperty(ref _TotalBeforeDiscount, value, "TotalBeforeDiscount"); }
        }

        private double _DiscountTotal;
        public double DiscountTotal
        {
            get { return _DiscountTotal; }
            set { SetProperty(ref _DiscountTotal, value, "DiscountTotal"); }
        }

        private double _TotalAfterDiscount;
        public double TotalAfterDiscount
        {
            get { return _TotalAfterDiscount; }
            set { SetProperty(ref _TotalAfterDiscount, value, "TotalAfterDiscount"); }
        }

        // Indexers
        public CartItem this[int index]
        {
            get { return Items[index]; }
            set { Items[index] = value; }
        }

        public CartItem this[Guid id]
        {
            get { return GetItem(id); }
        }

        // Methods
        public CartItem GetItem(Guid id)
        {
            return Items.FirstOrDefault(i => i.Id.Equals(id));
        }
    }
}