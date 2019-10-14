namespace Spending.Infrastructure.Repositories
{
    using Spending.Domain.Contracts;
    using Spending.Infrastructure.Data;
    using System.Collections.Generic;
    using System.Linq;

    public class SpenderRepository : ISpenderRepository
    {
        private ApplicationDbContext _applicationDbContext;

        public SpenderRepository(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }

        public Domain.Entity.Spender Get(long? id)
        {
            return _applicationDbContext.Spenders.SingleOrDefault(sp => sp.Id == id)?.ToEntity();
        }
    }
}
