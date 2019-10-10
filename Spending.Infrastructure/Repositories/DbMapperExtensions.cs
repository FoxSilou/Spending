namespace Spending.Infrastructure.Repositories
{
    public static class DbMapperExtensions
    {
        public static Domain.Models.SpendingDto ToModel(this Data.Spending spending)
        {
            return new Domain.Models.SpendingDto
            {
                Id = spending.Id,
                SpenderId = spending.SpenderId,
                Date = spending.Date,
                Nature = spending.Nature,
                Amount = spending.Amount,
                CurrencyId = spending.CurrencyId,
                Comment = spending.Comment,
            };
        }

        public static Domain.Models.SpenderDto ToModel(this Data.Spender spender)
        {
            return new Domain.Models.SpenderDto
            {
                Id = spender.Id,
                FirstName = spender.FirstName,
                LastName = spender.LastName,
                CurrencyId = spender.CurrencyId,
            };
        }
        public static Domain.Models.CurrencyDto ToModel(this Data.Currency currency)
        {
            return new Domain.Models.CurrencyDto
            {
                Id = currency.Id,
                Name = currency.Name,
            };
        }

        public static Data.Spending ToDb(this Domain.Models.SpendingDto spending)
        {
            return new Data.Spending
            {
                Id = spending.Id,
                SpenderId = spending.SpenderId,
                Date = spending.Date,
                Nature = spending.Nature,
                Amount = spending.Amount,
                CurrencyId = spending.CurrencyId,
                Comment = spending.Comment,
            };
        }

        public static Data.Spender ToDb(this Domain.Models.SpenderDto spender)
        {
            return new Data.Spender
            {
                Id = spender.Id,
                FirstName = spender.FirstName,
                LastName = spender.LastName,
                CurrencyId = spender.CurrencyId,
            };
        }
        public static Data.Currency ToDb(this Domain.Models.CurrencyDto currency)
        {
            return new Data.Currency
            {
                Id = currency.Id,
                Name = currency.Name,
            };
        }
    }
}
