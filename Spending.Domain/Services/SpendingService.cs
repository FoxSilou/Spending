namespace Spending.Domain.Services
{
    using Spending.Domain.Contracts;
    using Spending.Domain.Exceptions;
    using Spending.Domain.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SpendingService : ISpendingService
    {
        private readonly ISpendingRepository _spendingRepository;
        private readonly ISpenderRepository _spenderRepository;
        private readonly ICurrencyRepository _currencyRepository;

        public SpendingService(ISpendingRepository spendingRepository, ISpenderRepository spenderRepository, ICurrencyRepository currencyRepository)
        {
            _spendingRepository = spendingRepository;
            _spenderRepository = spenderRepository;
            _currencyRepository = currencyRepository;
        }

        public SpendingViewModel Get(long? id)
        {
            Models.SpendingDto spending = _spendingRepository.Get(id);
            if (spending == null)
            {
                throw new NotFoundException("Spending not found");
            }

            Models.CurrencyDto currency = _currencyRepository.Get(spending.CurrencyId);
            Models.SpenderDto spender = _spenderRepository.Get(spending.SpenderId);

            return spending.ToViewModel(spender, currency);
        }

        public IEnumerable<SpendingViewModel> GetFromSpender(long spenderId)
        {
            Models.SpenderDto spender = _spenderRepository.Get(spenderId);
            if (spender == null)
            {
                throw new NotFoundException("Spender not found");
            }

            IDictionary<long, Models.SpendingDto> spendings = _spendingRepository.GetFromSpender(spenderId);
            IDictionary<long, Models.CurrencyDto> currencies = _currencyRepository.Get();

            foreach (Models.SpendingDto spending in spendings.Values)
            {
                yield return spending.ToViewModel(spender, spending.CurrencyId.HasValue ? currencies[spending.CurrencyId.Value] : null);
            }
        }

        public async Task<SpendingViewModel> Create(CreateSpendingViewModel spendingToAdd)
        {
            Models.CurrencyDto currency = _currencyRepository.Get(spendingToAdd.CurrencyId);
            if (currency == null)
            {
                throw new NotFoundException("Currency not found");
            }

            Models.SpenderDto spender = _spenderRepository.Get(spendingToAdd.SpenderId);
            if (spender == null)
            {
                throw new NotFoundException("Spender not found");
            }

            Entity.Spending spendingToCreate = Entity.Spending.BuildSpending(
                amount: spendingToAdd.Amount,
                comment: spendingToAdd.Comment,
                currencyId: currency?.Id, 
                currencyName: currency?.Name,
                spenderId: spender?.Id, 
                firstName: spender?.FirstName, 
                lastName: spender?.LastName,
                spenderCurrencyId: spender.CurrencyId,
                date: spendingToAdd.Date,
                nature: spendingToAdd.Nature);

            ValidateNewSpending(spendingToCreate);

            Models.SpendingDto inserted = await _spendingRepository.Insert(new Models.SpendingDto
            {
                Id = spendingToCreate.Id,
                SpenderId = spendingToCreate.Spender?.Id,
                Date = spendingToCreate.Date.Date,
                Nature = spendingToCreate.Nature,
                Amount = spendingToCreate.Amount?.Value,
                CurrencyId = spendingToCreate.Currency?.Id,
                Comment = spendingToCreate.Comment,
            });

            return inserted.ToViewModel(spender, currency);
        }

        private void ValidateDoesNotAlreadyExists(Entity.Spending spendingToValidate)
        {
            if (spendingToValidate.Spender?.Id == null)
            {
                throw new ValidationException("Spending not valid: No Spender");
            }

            IDictionary<long, Models.SpendingDto> spenderSpendings = _spendingRepository.GetFromSpender(spendingToValidate.Spender.Id.Value);

            if (spenderSpendings.Values.Any(sp => Entity.Quantity.BuildQuantity(sp.Amount).IsEquals(spendingToValidate.Amount)
                && Entity.SpendingDate.BuildSpendingDate(sp.Date).IsEquals(spendingToValidate.Date)))
            {
                throw new ValidationException("Spending not valid: A spending that has the same amount and date already exists");
            }
        }

        private void ValidateNewSpending(Entity.Spending spendingToValidate)
        {
            spendingToValidate.ValidateNew(DateTime.UtcNow);
            ValidateDoesNotAlreadyExists(spendingToValidate);
        }
    }
}
