namespace Spending.Domain.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISpendingRepository
    {
        Models.SpendingDto Get(long? id);

        IDictionary<long, Domain.Models.SpendingDto> Get();

        IDictionary<long, Domain.Models.SpendingDto> GetFromSpender(long spenderId);

        Task<Models.SpendingDto> Insert(Models.SpendingDto spendingToAdd);
    }
}
