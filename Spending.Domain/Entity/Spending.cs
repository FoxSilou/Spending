namespace Spending.Domain.Entity
{
    using global::Spending.Domain.Exceptions;
    using System;

    public class Spending
    {
        public long? Id { get; private set; }
        public Spender Spender { get; private set; }
        public SpendingDate Date { get; private set; }
        public SpendingNature Nature { get; private set; }
        public Quantity Amount { get; private set; }
        public Currency Currency { get; private set; }
        public string Comment { get; private set; }

        private Spending()
        {
        }

        public static Spending BuildSpending(
            long? id = null, 
            Spender spender = null,
            DateTime? date = null, 
            SpendingNature nature = SpendingNature.Misc, 
            double? amount = null,
            Currency currency = null,
            string comment = null)
        {
            return new Spending()
                .SetId(id)
                .SetSpender(spender)
                .SetSpendingDate(date)
                .SetNature(nature)
                .SetAmount(amount)
                .SetCurrency(currency)
                .SetComment(comment);
        }

        public Spending SetId(long? id = null)
        {
            Id = id;
            return this;
        }

        public Spending SetSpender(Spender spender = null)
        {
            if (Currency != null
                && Currency.Id != spender?.Currency?.Id)
            {
                throw new ValidationException("Spending.SetSpender: Spender is not valid");
            }

            Spender = spender;
            return this;
        }

        public Spending SetSpendingDate(DateTime? date = null)
        {
            Date = SpendingDate.BuildSpendingDate(date);
            return this;
        }

        public Spending SetNature(SpendingNature nature = SpendingNature.Misc)
        {
            Nature = nature;
            return this;
        }

        public Spending SetAmount(double? amount = null)
        {
            Amount = Quantity.BuildQuantity(amount);
            return this;
        }

        public Spending SetCurrency(Currency curency = null)
        {
            if (Spender != null 
                && Spender.Currency.Id != curency?.Id)
            {
                throw new ValidationException("Spending.SetCurrency: Currency is not valid");
            }

            Currency = curency;
            return this;
        }

        public Spending SetComment(string comment)
        {
            if (string.IsNullOrWhiteSpace(comment))
            {
                throw new ValidationException("Spending.SetComment: Comment is not valid");
            }

            Comment = comment;
            return this;
        }

        public void ValidateNew(DateTime now)
        {
            if (this.Id != null) return; // If the spending has aldready been persisted, this validation has no sense

            Date.ValidateNew(now);
        }
    }
}
