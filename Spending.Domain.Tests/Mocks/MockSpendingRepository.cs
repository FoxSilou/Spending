namespace Spending.Domain.Tests.Mocks
{
    using Moq;
    using Spending.Domain.Contracts;
    using Spending.Domain.Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MockSpendingRepository : Mock<ISpendingRepository>
    {
        private static readonly Random _random = new Random();

        public Models.SpendingDto SetupGet(Models.SpendingDto returnedSpending)
        {
            Setup(x => x.Get(returnedSpending.Id)).Returns(returnedSpending);

            return returnedSpending;
        }

        public void SetupGet(params Models.SpendingDto[] spendings)
        {
            Setup(x => x.Get()).Returns(spendings.ToDictionary(sp => sp.Id.Value));
        }

        public void SetupGetFromSpender(long spenderId, params Models.SpendingDto[] spendings)
        {
            Setup(x => x.GetFromSpender(spenderId)).Returns(spendings.ToDictionary(sp => sp.Id.Value));
        }

        public void SetupInsert()
        {
            Setup(x => x.Insert(It.IsAny<Models.SpendingDto>())).ReturnsAsync((Models.SpendingDto sp) => sp);
        }

        public Models.SpendingDto GenerateSpendingDto(long? id = null, DateTime? date = null, long? spenderId = null, double? amount = null, long? currencyId = null)
         {
            var generatedSpending = new Models.SpendingDto
            {
                Id = id ?? _random.Next(),
                SpenderId = spenderId ?? _random.Next(),
                Date = date ?? DateTime.Now.AddDays(_random.Next(-3, 3)),
                Nature = (SpendingNature) Enum.ToObject(typeof(SpendingNature), _random.Next(0, 2)),
                Amount = amount ?? _random.Next(),
                CurrencyId = currencyId ?? _random.Next(),
                Comment = Guid.NewGuid().ToString(),
            };

            Setup(x => x.Get(generatedSpending.Id)).Returns(generatedSpending);

            return generatedSpending;
        }
    }
}
