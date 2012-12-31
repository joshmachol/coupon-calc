using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace CouponCalc.Model
{
    public enum Store
    {
        Publix = 0,
        Target = 1,
        Walmart = 2,
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
            get { return _Items ?? (_Items = new ObservableCollection<CartItem>()); }
            set { SetProperty(ref _Items, value, "Items"); }
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