namespace Spending.ViewModels
{
    using Spending.Domain.Entity;
    using Spending.Domain.Enum;
    using System;

    public class CreateSpendingViewModel
    {
        public long SpenderId { get; set; }
        public DateTime? Date { get; set; }
        public SpendingNature Nature { get; set; }
        public decimal? Amount { get; set; }
        public long CurrencyId { get; set; }
        public string Comment { get; set; }
    }
}
