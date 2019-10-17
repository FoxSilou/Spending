using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spending.Infrastructure.Data
{
    public static class ApplicationDbContextSeeding
    {
        public static async Task EnsureSeedData(this ApplicationDbContext context)
        {
            using (context)
            {
                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    bool hasChanged = false;

                    List<Currency> currencies = new List<Currency>
                    {
                        new Currency { Name = "Dollar américain" },
                        new Currency { Name = "Rouble russe" },
                    };

                    var missingCurrencies = currencies.Where(x => !context.Currencies.Any(y => y.Name == x.Name)).ToArray();
                    if (missingCurrencies.Any())
                    {
                        context.Currencies.AddRange(missingCurrencies);
                        hasChanged = true;
                    }

                    Currency dollar = currencies.FirstOrDefault(c => c.Name == "Dollar américain");
                    Currency rouble = currencies.FirstOrDefault(c => c.Name == "Rouble russe");

                    List<Spender> spenders = new List<Spender>
                    {
                        new Spender { FirstName = "Anthony", LastName = "Stark", Currency = dollar },
                        new Spender { FirstName = "Natasha", LastName = "Romanova", Currency = rouble },
                    };

                    var missingSpenders = spenders.Where(x => !context.Spenders.Any(y => y.FirstName == x.FirstName && y.LastName == x.LastName)).ToArray();
                    if (missingSpenders.Any())
                    {
                        context.Spenders.AddRange(missingSpenders);
                        hasChanged = true;
                    }

                    if (hasChanged)
                    {
                        await context.SaveChangesAsync();
                        transaction.Commit();
                    }
                }
            }
        }
    }
}
