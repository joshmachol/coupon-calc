using Microsoft.Phone.Controls.Primitives;
using System;
using System.Windows.Controls;

namespace CouponCalc.Common
{
    public abstract class LoopingDataSourceBase : ILoopingSelectorDataSource
    {
        private object _SelectedItem;

        #region ILoopingSelectorDataSource Members

        public abstract object GetNext(object relativeTo);

        public abstract object GetPrevious(object relativeTo);

        public object SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                // this will use the Equals method if it is overridden for the data source item class
                if (!object.Equals(_SelectedItem, value))
                {
                    // save the previously selected item so that we can use it 
                    // to construct the event arguments for the SelectionChanged event
                    object previousSelectedItem = _SelectedItem;
                    _SelectedItem = value;
                    // fire the SelectionChanged event
                    OnSelectionChanged(previousSelectedItem, _SelectedItem);
                }
            }
        }

        public event EventHandler<SelectionChangedEventArgs> SelectionChanged;

        protected virtual void OnSelectionChanged(object oldSelectedItem, object newSelectedItem)
        {
            EventHandler<SelectionChangedEventArgs> handler = SelectionChanged;
            if (handler != null)
            {
                handler(this, new SelectionChangedEventArgs(new[] { oldSelectedItem }, new[] { newSelectedItem }));
            }
        }

        #endregion
    }

    public class IntLoopingDataSource : LoopingDataSourceBase
    {
        private int _MinValue;
        private int _MaxValue;
        private int _Increment;

        public IntLoopingDataSource()
        {
            MaxValue = 10;
            MinValue = 0;
            Increment = 1;
            SelectedItem = 0;
        }

        public IntLoopingDataSource(int max, int min, int increment, int selected)
        {
            MaxValue = max;
            MinValue = min;
            Increment = increment;
            SelectedItem = selected;
        }

        public int MinValue
        {
            get { return _MinValue; }
            set
            {
                if (value >= MaxValue)
                    throw new ArgumentOutOfRangeException("MinValue", "MinValue cannot be equal or greater than MaxValue");
                _MinValue = value;
            }
        }

        public int MaxValue
        {
            get { return _MaxValue; }
            set
            {
                if (value <= MinValue)
                    throw new ArgumentOutOfRangeException("MaxValue", "MaxValue cannot be equal or lower than MinValue");
                _MaxValue = value;
            }
        }

        public int Increment
        {
            get { return _Increment; }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException("Increment", "Increment cannot be less than or equal to zero");
                _Increment = value;
            }
        }

        public override object GetNext(object relativeTo)
        {
            int nextValue = (int)relativeTo + Increment;
            if (nextValue > MaxValue)
            {
                nextValue = MinValue;
            }
            return nextValue;
        }

        public override object GetPrevious(object relativeTo)
        {
            int prevValue = (int)relativeTo - Increment;
            if (prevValue < MinValue)
            {
                prevValue = MaxValue;
            }
            return prevValue;
        }
    }
}