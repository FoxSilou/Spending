namespace Spending.Infrastructure.Repositories
{
    using Spending.Domain.Contracts;
    using Spending.Infrastructure.Data;
    using System.Collections.Generic;
    using System.Linq;

    public class CurrencyRepository : ICurrencyRepository
    {
        private ApplicationDbContext _applicationDbContext;

        public CurrencyRepository(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }

        public Domain.Entity.Currency Get(long? id)
        {
            return _applicationDbContext.Currencies.SingleOrDefault(sp => sp.Id == id)?.ToEntity();
        }
    }
}
