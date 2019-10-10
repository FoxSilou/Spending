using System.Collections.Generic;

namespace Spending.Domain.Contracts
{
    public interface ISpenderRepository
    {
        Models.SpenderDto Get(long? id);

        IDictionary<long, Models.SpenderDto> Get();
    }
}
