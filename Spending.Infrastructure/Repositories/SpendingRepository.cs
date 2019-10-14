namespace Spending.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Spending.Domain.Contracts;
    using Spending.Infrastructure.Data;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SpendingRepository : ISpendingRepository
    {
        private ApplicationDbContext _applicationDbContext;

        public SpendingRepository(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }

        public Domain.Entity.Spending Get(long? id)
        {
            return _applicationDbContext.Spendings
                .Include(sp => sp.Spender)
                .Include(sp => sp.Currency)
                .SingleOrDefault(sp => sp.Id == id)?.ToEntity();
        }

        public IList<Domain.Entity.Spending> Get()
        {
            return _applicationDbContext.Spendings
                .Include(sp => sp.Spender)
                .Include(sp => sp.Currency)
                .Select(sp => sp.ToEntity()).ToList();
        }

        public IList<Domain.Entity.Spending> GetFromSpender(long spenderId)
        {
            return _applicationDbContext.Spendings
                .Include(sp => sp.Spender)
                .Include(sp => sp.Currency)
                .Where(sp => sp.SpenderId == spenderId)
                .Select(sp => sp.ToEntity()).ToList();
        }

        public async Task<Domain.Entity.Spending> Insert(Domain.Entity.Spending spendingToAdd)
        {
            EntityEntry<Spending> result = await _applicationDbContext.Spendings.AddAsync(spendingToAdd.ToDb());
            await _applicationDbContext.SaveChangesAsync();

            return result.Entity.ToEntity();
        }
    }
}
