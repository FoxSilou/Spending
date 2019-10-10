namespace Spending.Domain.Tests.Mocks
{
    using Moq;
    using Spending.Domain.Contracts;
    using Spending.Domain.Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MockSpenderRepository : Mock<ISpenderRepository>
    {
        private static readonly Random _random = new Random();

        public void SetupGet(Models.SpenderDto returnedSpender)
        {
            Setup(x => x.Get(returnedSpender.Id)).Returns(returnedSpender);
        }

        public void SetupGet(params Models.SpenderDto[] spenders)
        {
            Setup(x => x.Get()).Returns(spenders.ToDictionary(spdr => spdr.Id.Value));
        }

        public Models.SpenderDto GenerateSpenderDto(long? id = null, long? currencyId = null)
        {
            var generatedSpender = new Models.SpenderDto
            {
                Id = id ?? _random.Next(),
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                CurrencyId = currencyId ?? _random.Next(),
            };

            Setup(x => x.Get(generatedSpender.Id)).Returns(generatedSpender);

            return generatedSpender;
        }
    }
}
