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
            long? spenderId = null, 
            string firstName = null, 
            string lastName = null,
            long? spenderCurrencyId = null,
            string spenderCurrencyName = null,
            DateTime? date = null, 
            SpendingNature nature = SpendingNature.Misc, 
            double? amount = null, 
            long? currencyId = null, 
            string currencyName = null,
            string comment = null)
        {
            return new Spending()
                .SetId(id)
                .SetSpender(spenderId: spenderId, firstName: firstName, lastName: lastName, currencyId: spenderCurrencyId, currencyName: spenderCurrencyName)
                .SetSpendingDate(date)
                .SetNature(nature)
                .SetAmount(amount)
                .SetCurrency(currencyId: currencyId, currencyName: currencyName)
                .SetComment(comment);
        }

        public Spending SetId(long? id = null)
        {
            Id = id;
            return this;
        }

        public Spending SetSpender(long? spenderId = null, string firstName = null, string lastName = null, long? currencyId = null, string currencyName = null)
        {
            if (Currency != null
                && Currency.Id != currencyId)
            {
                throw new ValidationException("Spending.SetSpender: Spender is not valid");
            }

            Spender = Spender.BuildSpender(id: spenderId, currencyId: currencyId, currencyName: currencyName, firstName: firstName, lastName: lastName);
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

        public Spending SetCurrency(long? currencyId = null, string currencyName = null)
        {
            if (Spender != null 
                && Spender.Currency.Id != currencyId)
            {
                throw new ValidationException("Spending.SetCurrency: Currency is not valid");
            }

            Currency = Currency.BuildCurrency(id: currencyId, name: currencyName);
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
