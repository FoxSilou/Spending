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

        public Domain.Models.CurrencyDto Get(long? id)
        {
            return _applicationDbContext.Currencies.SingleOrDefault(sp => sp.Id == id)?.ToModel();
        }

        public IDictionary<long, Domain.Models.CurrencyDto> Get()
        {
            return _applicationDbContext.Currencies.Select(sp => sp.ToModel()).ToDictionary(sp => sp.Id.Value);
        }

        public IDictionary<long, Domain.Models.CurrencyDto> GetFromIds(ISet<long> ids)
        {
            return _applicationDbContext.Currencies.Where(sp => ids.Contains(sp.Id.Value)).Select(sp => sp.ToModel()).ToDictionary(sp => sp.Id.Value);
        }
    }
}
