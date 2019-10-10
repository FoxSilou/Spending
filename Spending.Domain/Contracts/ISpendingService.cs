namespace Spending.Domain.Contracts
{
    using Spending.Domain.ViewModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISpendingService
    {
        SpendingViewModel Get(long? id);

        IEnumerable<SpendingViewModel> GetFromSpender(long SpenderId);

        Task<SpendingViewModel> Create(CreateSpendingViewModel spendingToAdd);
    }
}
