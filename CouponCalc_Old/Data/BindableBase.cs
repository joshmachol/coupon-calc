using CouponCalc.Annotations;
using System.Collections.Generic;
using System.ComponentModel;

namespace CouponCalc.Data
{
    public class BindableBase : IdentifiableBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
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