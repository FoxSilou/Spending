using System;

namespace Spending.Infrastructure.Data
{
    public class Spending
    {
        public long? Id { get; set; }
        public Spender Spender { get; set; }
        public long? SpenderId { get; set; }
        public DateTime? Date { get; set; }
        public Domain.Entity.SpendingNature Nature { get; set; }
        public double? Amount { get; set; }
        public Currency Currency { get; set; }
        public long? CurrencyId { get; set; }
        public string Comment { get; set; }
    }
}
