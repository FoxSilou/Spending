namespace Spending.Domain.Services
{
    using Spending.Domain.Contracts;
    using Spending.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SpendingService : ISpendingService
    {
        private readonly ISpendingRepository _spendingRepository;

        public SpendingService(ISpendingRepository spendingRepository)
        {
            _spendingRepository = spendingRepository;
        }

        public void ValidateNewSpending(Entity.Spending spendingToValidate)
        {
            spendingToValidate.ValidateNew(DateTime.UtcNow);
            ValidateDoesNotAlreadyExists(spendingToValidate);
        }

        private void ValidateDoesNotAlreadyExists(Entity.Spending spendingToValidate)
        {
            if (spendingToValidate.Spender?.Id == null)
            {
                throw new ValidationException("Spending not valid: No Spender");
            }

            IList<Entity.Spending> spenderSpendings = _spendingRepository.GetFromSpender(spendingToValidate.Spender.Id.Value);

            if (spenderSpendings.Any(sp => sp.Amount.IsEquals(spendingToValidate.Amount)
                && sp.Date.IsEquals(spendingToValidate.Date)))
            {
                throw new ValidationException("Spending not valid: A spending that has the same amount and date already exists");
            }
        }
    }
}
