using CouponCalc.Model;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

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

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}