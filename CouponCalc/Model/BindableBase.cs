using CouponCalc.Data;
using System.Collections.Generic;
using System.ComponentModel;

namespace CouponCalc.Model
{
    public class BindableBase : IdentifiableBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetProperty<T>(ref T value, T temp, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(value, temp))
                return;
            value = temp;
            OnPropertyChanged(propertyName);
        }
    }
}