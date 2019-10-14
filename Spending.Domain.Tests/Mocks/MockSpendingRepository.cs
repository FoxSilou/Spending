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

        public Spending SetupGet(Spending returnedSpending)
        {
            Setup(x => x.Get(returnedSpending.Id)).Returns(returnedSpending);

            return returnedSpending;
        }

        public void SetupGet(long id, Spending spending)
        {
            Setup(x => x.Get(id)).Returns(spending);
        }

        public void SetupGet(params Spending[] spendings)
        {
            Setup(x => x.Get()).Returns(spendings);
        }

        public void SetupGetFromSpender(long spenderId, params Spending[] spendings)
        {
            Setup(x => x.GetFromSpender(spenderId)).Returns(spendings);
        }

        public void SetupInsert()
        {
            Setup(x => x.Insert(It.IsAny<Spending>())).ReturnsAsync((Spending sp) => sp);
        }
    }
}
