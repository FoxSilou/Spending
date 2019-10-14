using System.Collections.Generic;

namespace Spending.Domain.Contracts
{
    public interface ISpenderRepository
    {
        Entity.Spender Get(long? id);
    }
}
