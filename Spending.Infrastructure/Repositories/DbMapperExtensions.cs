namespace Spending.Infrastructure.Repositories
{
    public static class DbMapperExtensions
    {
        public static Domain.Entity.Spending ToEntity(this Data.Spending spending)
        {
            return Domain.Entity.Spending.BuildSpending(
                id: spending.Id,
                amount: spending.Amount,
                comment: spending.Comment,
                currency: Domain.Entity.Currency.BuildCurrency(
                    id: spending.CurrencyId,
                    name: spending.Currency?.Name),
                spender: Domain.Entity.Spender.BuildSpender(
                    id: spending.SpenderId,
                    currency: Domain.Entity.Currency.BuildCurrency(
                        id: spending.Spender?.CurrencyId,
                        name: spending.Spender?.Currency?.Name),
                    firstName: spending.Spender?.FirstName,
                    lastName: spending.Spender?.LastName),
                date: spending.Date,
                nature: spending.Nature);
        }

        public static Domain.Entity.Spender ToEntity(this Data.Spender spender)
        {
            return Domain.Entity.Spender.BuildSpender(
                id: spender.Id,
                currency: Domain.Entity.Currency.BuildCurrency(
                    id: spender.CurrencyId, 
                    name: spender.Currency?.Name),
                firstName: spender.FirstName,
                lastName: spender.LastName);
        }
        public static Domain.Entity.Currency ToEntity(this Data.Currency currency)
        {
            return Domain.Entity.Currency.BuildCurrency(
                id: currency.Id,
                name: currency.Name);
        }

        public static Data.Spending ToDb(this Domain.Entity.Spending spending)
        {
            return new Data.Spending
            {
                SpenderId = spending.Spender.Id.Value,
                Date = spending.Date.Date.Value,
                Nature = spending.Nature,
                Amount = spending.Amount.Value.Value,
                CurrencyId = spending.Currency.Id.Value,
                Comment = spending.Comment,
            };
        }

        public static Data.Spender ToDb(this Domain.Entity.Spender spender)
        {
            return new Data.Spender
            {
                FirstName = spender.Person.FirstName,
                LastName = spender.Person.LastName,
                CurrencyId = spender.Currency.Id.Value,
            };
        }

        public static Data.Currency ToDb(this Domain.Entity.Currency currency)
        {
            return new Data.Currency
            {
                Name = currency.Name,
            };
        }
    }
}
