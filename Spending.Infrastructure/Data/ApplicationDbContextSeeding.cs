using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spending.Infrastructure.Data
{
    public class ApplicationDbContextSeeding
    {
        private readonly Func<ApplicationDbContext> _applicationDbContextFactory;

        public ApplicationDbContextSeeding(Func<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory;
        }

        public async Task EnsureSeedData()
        {
            using (var applicationDbContext = _applicationDbContextFactory())
            {
                using (var transaction = await applicationDbContext.Database.BeginTransactionAsync())
                {
                    bool hasChanged = false;

                    List<Currency> currencies = new List<Currency>
                    {
                        new Currency { Name = "Dollar américain" },
                        new Currency { Name = "Rouble russe" },
                    };

                    var missingCurrencies = currencies.Where(x => !applicationDbContext.Currencies.Any(y => y.Name == x.Name)).ToArray();
                    if (missingCurrencies.Any())
                    {
                        applicationDbContext.Currencies.AddRange(missingCurrencies);
                        hasChanged = true;
                    }

                    Currency dollar = currencies.FirstOrDefault(c => c.Name == "Dollar américain");
                    Currency rouble = currencies.FirstOrDefault(c => c.Name == "Rouble russe");

                    List<Spender> spenders = new List<Spender>
                    {
                        new Spender { FirstName = "Anthony", LastName = "Stark", Currency = dollar },
                        new Spender { FirstName = "Natasha", LastName = "Romanova", Currency = rouble },
                    };

                    var missingSpenders = spenders.Where(x => !applicationDbContext.Spenders.Any(y => y.FirstName == x.FirstName && y.LastName == x.LastName)).ToArray();
                    if (missingSpenders.Any())
                    {
                        applicationDbContext.Spenders.AddRange(missingSpenders);
                        hasChanged = true;
                    }

                    if (hasChanged)
                    {
                        await applicationDbContext.SaveChangesAsync();
                        transaction.Commit();
                    }
                }
            }
        }
    }
}
