using Spending.Domain.Exceptions;
using System;

namespace Spending.Domain.Entity
{
    public class Quantity
    {
        public double? Value { get; private set; }

        private Quantity()
        {
        }

        public static Quantity BuildQuantity(
            double? value = null)
        {
            return new Quantity().SetValue(value);
        }

        public Quantity SetValue(double? value = null)
        {
            if (!value.HasValue)
            {
                throw new ValidationException("Quantity is not valid");
            }

            Value = value;
            return this;
        }
        
        public bool IsEquals(Quantity quantityToCompare)
        {
            return quantityToCompare.Value  == Value;
        }
    }
}
