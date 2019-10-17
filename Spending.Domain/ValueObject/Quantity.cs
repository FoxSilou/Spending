namespace Spending.Domain.ValueObject
{
    using Spending.Domain.Exceptions;

    public struct Quantity
    {
        public decimal? Value { get; private set; }

        public Quantity(decimal? value)
        {
            if (!value.HasValue)
            {
                throw new ValidationException("Quantity is not valid");
            }

            Value = value;
        }
        
        public bool IsEquals(Quantity quantityToCompare)
        {
            return quantityToCompare.Value  == Value;
        }
    }
}
