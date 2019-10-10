namespace Spending.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Spending.Domain.Contracts;
    using Spending.Infrastructure.Data;
    using System;
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

        public Domain.Models.SpendingDto Get(long? id)
        {
            return _applicationDbContext.Spendings.SingleOrDefault(sp => sp.Id == id)?.ToModel();
        }

        public IDictionary<long, Domain.Models.SpendingDto> Get()
        {
            return _applicationDbContext.Spendings.Select(sp => sp.ToModel()).ToDictionary(sp => sp.Id.Value);
        }

        public IDictionary<long, Domain.Models.SpendingDto> GetFromSpender(long spenderId)
        {
            return _applicationDbContext.Spendings.Where(sp => sp.SpenderId == spenderId).Select(sp => sp.ToModel()).ToDictionary(sp => sp.Id.Value);
        }

        public async Task<Domain.Models.SpendingDto> Insert(Domain.Models.SpendingDto spendingToAdd)
        {
            EntityEntry<Spending> result = await _applicationDbContext.Spendings.AddAsync(spendingToAdd.ToDb());
            await _applicationDbContext.SaveChangesAsync();

            return result.Entity.ToModel();
        }
    }
}
