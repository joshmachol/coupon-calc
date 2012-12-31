using System;
using System.Collections.Generic;

namespace CouponCalc.Model
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
        void GetCarts(Action<IEnumerable<Cart>, Exception> callback);
    }
}