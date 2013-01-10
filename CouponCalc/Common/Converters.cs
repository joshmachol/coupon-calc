using CouponCalc.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace CouponCalc.Common
{
    public class ValueEqualsConverterParam : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BoolToTransparentOrAccentBrush : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is bool))
                return DependencyProperty.UnsetValue;
            return (bool)value ? new SolidColorBrush(Colors.Transparent) : App.Current.Resources["PhoneAccentBrush"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CartItemToTotalEquationString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var cartItem = value as CartItem;
            if (cartItem == null)
                return DependencyProperty.UnsetValue;
            var price = cartItem.Price;
            var discount = cartItem.Discounts.Sum(d => d.Discount);
            var total = price - discount;
            return string.Format("{0:c2} - {1:c2} = {2:c2}", price, discount, total);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StoreToImageSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is Store))
                return DependencyProperty.UnsetValue;
            Store store = (Store)value;
            return "/Assets/StoreLogos/logo_" + store + ".png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CartsToUniqueStoreList : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var carts = value as IEnumerable<Cart>;
            if (carts == null)
                return DependencyProperty.UnsetValue;
            return carts.GroupBy(c => c.Store)
                        .OrderBy(g => g.Key)
                        .Select(g => new
                        {
                            Store = g.Key,
                            CartCount = g.Count(),
                        });
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class EnumToIEnumerable : IValueConverter
    {
        private readonly Dictionary<Type, List<object>> _cache = new Dictionary<Type, List<object>>();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var type = value.GetType();
            if (!_cache.ContainsKey(type))
            {
                var fields = type.GetFields().Where(field => field.IsLiteral);
                var values = new List<object>();
                foreach (var field in fields)
                {
                    DescriptionAttribute[] a = field.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
                    if (a != null && a.Length > 0)
                    {
                        values.Add(a[0].Description);
                    }
                    else
                    {
                        values.Add(field.GetValue(value));
                    }
                }
                _cache[type] = values.OrderBy(o => o.ToString()).ToList();
            }

            return _cache[type];
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}