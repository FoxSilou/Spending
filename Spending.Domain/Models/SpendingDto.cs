namespace Spending.Domain.Models
{
    using System;

    public class SpendingDto
    {
        public long? Id { get; set; }
        public long? SpenderId { get; set; }
        public DateTime? Date { get; set; }
        public Entity.SpendingNature Nature { get; set; }
        public double? Amount { get; set; }
        public long? CurrencyId { get; set; }
        public string Comment { get; set; }
    }
}
