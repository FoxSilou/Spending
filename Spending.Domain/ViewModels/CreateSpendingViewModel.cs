namespace Spending.Domain.ViewModels
{
    using Spending.Domain.Entity;
    using System;

    public class CreateSpendingViewModel
    {
        public long SpenderId { get; set; }
        public DateTime? Date { get; set; }
        public SpendingNature Nature { get; set; }
        public double? Amount { get; set; }
        public long CurrencyId { get; set; }
        public string Comment { get; set; }
    }
}
