
namespace CouponCalc.Data
{
    public enum DiscountType
    {
        Store,
        Manufacturer,
    }

    public class CartItemDiscount : BindableBase
    {
        private DiscountType _Type;
        public DiscountType Type
        {
            get { return _Type; }
            set { SetProperty(ref _Type, value, "Type"); }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { SetProperty(ref _Name, value, "Name"); }
        }

        private double _Discount;
        public double Discount
        {
            get { return _Discount; }
            set { SetProperty(ref _Discount, value, "Discount"); }
        }
    }
}