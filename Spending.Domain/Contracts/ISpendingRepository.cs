namespace Spending.Domain.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISpendingRepository
    {
        Entity.Spending Get(long? id);

        IList<Entity.Spending> Get();

        IList<Entity.Spending> GetFromSpender(long spenderId);

        Task<Entity.Spending> Insert(Entity.Spending spendingToAdd);
    }
}
