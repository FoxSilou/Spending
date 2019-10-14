namespace Spending.Domain.Tests
{
    using FluentAssertions;
    using Moq;
    using Spending.Domain.Entity;
    using Spending.Domain.Exceptions;
    using Spending.Domain.Services;
    using Spending.Domain.Tests.Mocks;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class SpendingTest
    {

        [Fact]
        public void Build_WithValidData_ShouldNotThrow()
        {
            // Act
            Func<Spending> action = () => TestHelpers.BuildRandomValidPending();

            // Assert
            action.Should().NotThrow();
        }

        [Fact]
        public void Build_WithNullComment_ShouldThrow()
        {
            // Act
            Func<Spending> action = () => TestHelpers.BuildRandomValidPending().SetComment(null);

            // Assert
            action.Should().Throw<ValidationException>();
        }

        [Fact]
        public void Build_WithEmptyComment_ShouldThrow()
        {
            // Act
            Func<Spending> action = () => TestHelpers.BuildRandomValidPending().SetComment(string.Empty);

            // Assert
            action.Should().Throw<ValidationException>();
        }

        [Fact]
        public void Build_WithOnlySpacesComment_ShouldThrow()
        {
            // Act
            Func<Spending> action = () => TestHelpers.BuildRandomValidPending().SetComment("   ");

            // Assert
            action.Should().Throw<ValidationException>();
        }

        [Fact]
        public void Build_WithCurrencyDiffersFromSpenders_ShouldThrow()
        {
            // Act
            Func<Spending> action = () => TestHelpers.BuildRandomValidPending().SetSpender(Spender.BuildSpender(currency: TestHelpers.BuildRandomValidCurrency()));

            // Assert
            action.Should().Throw<ValidationException>();
        }

        [Fact]
        public void Build_WithSpendersCurrencyDiffers_ShouldThrow()
        {
            // Act
            Func<Spending> action = () => TestHelpers.BuildRandomValidPending().SetCurrency(TestHelpers.BuildRandomValidCurrency());

            // Assert
            action.Should().Throw<ValidationException>();
        }
    }
}
