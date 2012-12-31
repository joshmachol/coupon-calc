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
    }
}