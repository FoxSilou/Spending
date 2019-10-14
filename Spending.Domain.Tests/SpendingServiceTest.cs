namespace Spending.Domain.Tests
{
    using FluentAssertions;
    using Spending.Domain.Entity;
    using Spending.Domain.Exceptions;
    using Spending.Domain.Services;
    using Spending.Domain.Tests.Mocks;
    using System;
    using Xunit;

    public class SpendingServiceTest
    {
        private static readonly Random _random = new Random();

        private readonly MockSpendingRepository _spendingRepositoryMock = new MockSpendingRepository();

        [Fact]
        public void ValidateNewSpending_WithValidModel_ShouldNotThrow()
        {
            // Arrange
            var spending = TestHelpers.BuildRandomValidPending();

            _spendingRepositoryMock.SetupGetFromSpender(spending.Spender.Id.Value, new Spending[] { });
            _spendingRepositoryMock.SetupInsert();

            // Act
            Action action = () => SetupTestedService().ValidateNewSpending(spending);

            // Assert
            action.Should().NotThrow();
        }

        [Fact]
        public void ValidateNewSpending_SpendingWithFutureDate_ShouldThrow()
        {
            // Arrange
            var spending = TestHelpers.BuildRandomValidPending().SetSpendingDate(TestHelpers.GetRandomDate(dayStart: 1, dayEnd: 180)); // Date is in the future

            _spendingRepositoryMock.SetupGetFromSpender(spending.Spender.Id.Value, new Spending[] { });
            _spendingRepositoryMock.SetupInsert();

            // Act
            Action action = () => SetupTestedService().ValidateNewSpending(spending);

            // Assert
            action.Should().Throw<ValidationException>();
        }

        [Fact]
        public void ValidateNewSpending_SpendingWithMoreThanThreeMontOldDate_ShouldThrow()
        {
            // Arrange
            var spending = TestHelpers.BuildRandomValidPending().SetSpendingDate(TestHelpers.GetRandomDate(dayStart: -180, dayEnd: -92)); // Date is more than three months old// Arrange

            _spendingRepositoryMock.SetupGetFromSpender(spending.Spender.Id.Value, new Spending[] { });
            _spendingRepositoryMock.SetupInsert();

            // Act
            Action action = () => SetupTestedService().ValidateNewSpending(spending);

            // Assert
            action.Should().Throw<ValidationException>();
        }

        [Fact]
        public void ValidateNewSpending_SpendingThatDuplicatesAnother_ShouldThrow()
        {
            // Arrange
            var spending = TestHelpers.BuildRandomValidPending();
            var existingSpending = TestHelpers.BuildRandomValidPending().SetAmount(spending.Amount.Value).SetSpendingDate(spending.Date.Date);

            _spendingRepositoryMock.SetupGetFromSpender(spending.Spender.Id.Value, existingSpending);
            _spendingRepositoryMock.SetupInsert();

            // Act
            Action action = () => SetupTestedService().ValidateNewSpending(spending);

            // Assert
            action.Should().Throw<ValidationException>();
        }

        private SpendingService SetupTestedService()
        {
            return new SpendingService(_spendingRepositoryMock.Object);
        }
    }
}
