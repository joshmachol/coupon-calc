using System;

namespace CouponCalc.Data
{
    public class IdentifiableBase
    {
        public Guid Id { get; private set; }

        public IdentifiableBase()
        {
            Id = Guid.NewGuid();
        }
    }
}