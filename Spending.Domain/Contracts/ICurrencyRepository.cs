using System.Collections.Generic;

namespace Spending.Domain.Contracts
{
    public interface ICurrencyRepository
    {
        Models.CurrencyDto Get(long? id);

        IDictionary<long, Models.CurrencyDto> Get();
    }
}
