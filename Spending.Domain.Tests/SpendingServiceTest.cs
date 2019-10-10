namespace Spending.Domain.Tests
{
    using FluentAssertions;
    using Moq;
    using Spending.Domain.Entity;
    using Spending.Domain.Services;
    using Spending.Domain.Tests.Mocks;
    using Spending.Domain.ViewModels;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class SpendingServiceTest
    {
        private static readonly Random _random = new Random();

        private readonly MockSpendingRepository _spendingRepositoryMock = new MockSpendingRepository();
        private readonly MockSpenderRepository _spenderRepositoryMock = new MockSpenderRepository();
        private readonly MockCurrencyRepository _currencyRepositoryMock = new MockCurrencyRepository();

        [Fact]
        public void Get_IdExists_ReturnsViewModel()
        {
            // Arrange
            var existingSpending = _spendingRepositoryMock.GenerateSpendingDto();

            var existingSpendingsCurrency = _currencyRepositoryMock.GenerateCurrencyDto(existingSpending.CurrencyId);
            var existingSpendingsSpender = _spenderRepositoryMock.GenerateSpenderDto(existingSpending.SpenderId, currencyId: existingSpendingsCurrency.Id);

            _spenderRepositoryMock.SetupGet(existingSpendingsSpender);
            _currencyRepositoryMock.SetupGet(existingSpendingsCurrency);

            // Act
            var result = SetupTestedService().Get(existingSpending.Id);

            // Assert
            var expected = new SpendingViewModel
            {
                Id = existingSpending.Id,
                SpenderFirstName = existingSpendingsSpender.FirstName,
                SpenderLastName = existingSpendingsSpender.LastName,
                Date = existingSpending.Date,
                Nature = existingSpending.Nature.ToString(),
                Amount = existingSpending.Amount,
                CurrencyName = existingSpendingsCurrency.Name,
                Comment = existingSpending.Comment,
            };

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async void Insert_ModelValid_ReturnsViewModel()
        {
            // Arrange
            var createViewModel = GenerateCreateViewModel(dayStart: -90, dayEnd: 0); // Date should be valid

            var existingSpendingsCurrency = _currencyRepositoryMock.GenerateCurrencyDto(createViewModel.CurrencyId);
            var existingSpendingsSpender = _spenderRepositoryMock.GenerateSpenderDto(createViewModel.SpenderId, currencyId: createViewModel.CurrencyId);

            _spenderRepositoryMock.SetupGet(existingSpendingsSpender);
            _currencyRepositoryMock.SetupGet(existingSpendingsCurrency);

            _spendingRepositoryMock.SetupGetFromSpender(createViewModel.SpenderId, new Models.SpendingDto[] { });
            _spendingRepositoryMock.SetupInsert();
            
            // Act
            var result = await SetupTestedService().Create(createViewModel);

            // Assert
            var expected = new SpendingViewModel
            {
                SpenderFirstName = existingSpendingsSpender.FirstName,
                SpenderLastName = existingSpendingsSpender.LastName,
                Date = createViewModel.Date,
                Nature = createViewModel.Nature.ToString(),
                Amount = createViewModel.Amount,
                CurrencyName = existingSpendingsCurrency.Name,
                Comment = createViewModel.Comment,
            };

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Insert_SpendingWithFutureDate_ThrowsException()
        {
            // Arrange
            var createViewModel = GenerateCreateViewModel(dayStart: 1, dayEnd: 180); // Date is in the future

            var existingSpendingsCurrency = _currencyRepositoryMock.GenerateCurrencyDto(createViewModel.CurrencyId);
            var existingSpendingsSpender = _spenderRepositoryMock.GenerateSpenderDto(createViewModel.SpenderId, currencyId: createViewModel.CurrencyId);

            _spenderRepositoryMock.SetupGet(existingSpendingsSpender);
            _currencyRepositoryMock.SetupGet(existingSpendingsCurrency);

            _spendingRepositoryMock.SetupGetFromSpender(createViewModel.SpenderId, new Models.SpendingDto[] { });
            _spendingRepositoryMock.SetupInsert();

            // Act
            Func<Task> insertAction = async () => { await SetupTestedService().Create(createViewModel); };

            // Assert
            insertAction.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Insert_SpendingWithMoreThanThreeMontOldDate_ThrowsException()
        {
            // Arrange
            var createViewModel = GenerateCreateViewModel(dayStart: -180, dayEnd: -92); // Date is more than three months old

            var existingSpendingsCurrency = _currencyRepositoryMock.GenerateCurrencyDto(createViewModel.CurrencyId);
            var existingSpendingsSpender = _spenderRepositoryMock.GenerateSpenderDto(createViewModel.SpenderId, currencyId: createViewModel.CurrencyId);

            _spenderRepositoryMock.SetupGet(existingSpendingsSpender);
            _currencyRepositoryMock.SetupGet(existingSpendingsCurrency);

            _spendingRepositoryMock.SetupGetFromSpender(createViewModel.SpenderId, new Models.SpendingDto[] { });
            _spendingRepositoryMock.SetupInsert();

            // Act
            Func<Task> insertAction = async () => { await SetupTestedService().Create(createViewModel); };

            // Assert
            insertAction.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Insert_SpendingWithNullComment_ThrowsException()
        {
            // Arrange
            var createViewModel = GenerateCreateViewModel(dayStart: -90, dayEnd: 0); // Date should be valid
            createViewModel.Comment = null; // Comment is null

            var existingSpendingsCurrency = _currencyRepositoryMock.GenerateCurrencyDto(createViewModel.CurrencyId);
            var existingSpendingsSpender = _spenderRepositoryMock.GenerateSpenderDto(createViewModel.SpenderId, currencyId: createViewModel.CurrencyId);

            _spenderRepositoryMock.SetupGet(existingSpendingsSpender);
            _currencyRepositoryMock.SetupGet(existingSpendingsCurrency);

            _spendingRepositoryMock.SetupGetFromSpender(createViewModel.SpenderId, new Models.SpendingDto[] { });
            _spendingRepositoryMock.SetupInsert();

            // Act
            Func<Task> insertAction = async () => { await SetupTestedService().Create(createViewModel); };

            // Assert
            insertAction.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Insert_SpendingWithEmptyComment_ThrowsException()
        {
            // Arrange
            var createViewModel = GenerateCreateViewModel(dayStart: -90, dayEnd: 0, comment: string.Empty); // Date should be valid and comment is empty

            var existingSpendingsCurrency = _currencyRepositoryMock.GenerateCurrencyDto(createViewModel.CurrencyId);
            var existingSpendingsSpender = _spenderRepositoryMock.GenerateSpenderDto(createViewModel.SpenderId, currencyId: createViewModel.CurrencyId);

            _spenderRepositoryMock.SetupGet(existingSpendingsSpender);
            _currencyRepositoryMock.SetupGet(existingSpendingsCurrency);

            _spendingRepositoryMock.SetupGetFromSpender(createViewModel.SpenderId, new Models.SpendingDto[] { });
            _spendingRepositoryMock.SetupInsert();

            // Act
            Func<Task> insertAction = async () => { await SetupTestedService().Create(createViewModel); };

            // Assert
            insertAction.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Insert_SpendingWithOnlySpacesComment_ThrowsException()
        {
            // Arrange
            var createViewModel = GenerateCreateViewModel(dayStart: -90, dayEnd: 0, comment: "  "); // Date should be valid and comment is only space

            var existingSpendingsCurrency = _currencyRepositoryMock.GenerateCurrencyDto(createViewModel.CurrencyId);
            var existingSpendingsSpender = _spenderRepositoryMock.GenerateSpenderDto(createViewModel.SpenderId, currencyId: createViewModel.CurrencyId);

            _spenderRepositoryMock.SetupGet(existingSpendingsSpender);
            _currencyRepositoryMock.SetupGet(existingSpendingsCurrency);

            _spendingRepositoryMock.SetupGetFromSpender(createViewModel.SpenderId, new Models.SpendingDto[] { });
            _spendingRepositoryMock.SetupInsert();

            // Act
            Func<Task> insertAction = async () => { await SetupTestedService().Create(createViewModel); };

            // Assert
            insertAction.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Insert_SpendingThatDuplicatesAnother_ThrowsException()
        {
            // Arrange
            var createViewModel = GenerateCreateViewModel(dayStart: -90, dayEnd: 0); // Date should be valid

            var existingSpending = _spendingRepositoryMock.GenerateSpendingDto(spenderId: createViewModel.SpenderId, date: createViewModel.Date, amount: createViewModel.Amount);

            var existingSpendingsCurrency = _currencyRepositoryMock.GenerateCurrencyDto(createViewModel.CurrencyId);
            var existingSpendingsSpender = _spenderRepositoryMock.GenerateSpenderDto(createViewModel.SpenderId, currencyId: createViewModel.CurrencyId);

            _spenderRepositoryMock.SetupGet(existingSpendingsSpender);
            _currencyRepositoryMock.SetupGet(existingSpendingsCurrency);

            _spendingRepositoryMock.SetupGetFromSpender(createViewModel.SpenderId, new[] { existingSpending });
            _spendingRepositoryMock.SetupInsert();

            // Act
            Func<Task> insertAction = async () => { await SetupTestedService().Create(createViewModel); };

            // Assert
            insertAction.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Insert_SpendingWithCurrencyDiffersFromSpender_ThrowsException()
        {
            // Arrange
            var createViewModel = GenerateCreateViewModel(dayStart: -90, dayEnd: 0); // Date should be valid

            var existingSpendingsCurrency = _currencyRepositoryMock.GenerateCurrencyDto(createViewModel.CurrencyId);
            var existingSpendingsSpender = _spenderRepositoryMock.GenerateSpenderDto(createViewModel.SpenderId);

            _spenderRepositoryMock.SetupGet(existingSpendingsSpender);
            _currencyRepositoryMock.SetupGet(existingSpendingsCurrency);

            _spendingRepositoryMock.SetupGetFromSpender(createViewModel.SpenderId, new Models.SpendingDto[] { });
            _spendingRepositoryMock.SetupInsert();

            // Act
            Func<Task> insertAction = async () => { await SetupTestedService().Create(createViewModel); };

            // Assert
            insertAction.Should().Throw<InvalidOperationException>();
        }

        private SpendingService SetupTestedService()
        {
            return new SpendingService(_spendingRepositoryMock.Object, _spenderRepositoryMock.Object, _currencyRepositoryMock.Object);
        }

        private CreateSpendingViewModel GenerateCreateViewModel(long? spenderId = null, int dayStart = -180, int dayEnd = 180, double? amount = null, long? currencyId = null, string comment = null)
        {
            return new CreateSpendingViewModel
            {
                SpenderId = spenderId ?? _random.Next(),
                Date = DateTime.Now.AddDays(_random.Next(dayStart, dayEnd)),
                Nature = (SpendingNature)Enum.ToObject(typeof(SpendingNature), _random.Next(0, 2)),
                Amount = amount ?? _random.Next(),
                CurrencyId = currencyId ?? _random.Next(),
                Comment = comment ?? Guid.NewGuid().ToString(),
            };
        }
    }
}
