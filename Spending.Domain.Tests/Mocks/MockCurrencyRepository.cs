namespace Spending.Domain.Tests.Mocks
{
    using Moq;
    using Spending.Domain.Contracts;
    using Spending.Domain.Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MockCurrencyRepository : Mock<ICurrencyRepository>
    {
        private static readonly Random _random = new Random();

        public void SetupGet(Models.CurrencyDto returnedCurrency)
        {
            Setup(x => x.Get(returnedCurrency.Id)).Returns(returnedCurrency);
        }

        public void SetupGet(params Models.CurrencyDto[] currencies)
        {
            Setup(x => x.Get()).Returns(currencies.ToDictionary(cur => cur.Id.Value));
        }

        public Models.CurrencyDto GenerateCurrencyDto(long? id = null)
        {
            var generatedCurrency = new Models.CurrencyDto
            {
                Id = id ?? _random.Next(),
                Name = Guid.NewGuid().ToString(),
            };

            Setup(x => x.Get(generatedCurrency.Id)).Returns(generatedCurrency);

            return generatedCurrency;
        }
    }
}
