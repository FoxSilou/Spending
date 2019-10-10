using System;

namespace Spending.Domain.ViewModels
{
    public class SpendingViewModel
    {
        public long? Id { get; set; }
        public string SpenderName { get; set; }
        public DateTime? Date { get; set; }
        public string Nature { get; set; }
        public double? Amount { get; set; }
        public string CurrencyName { get; set; }
        public string Comment { get; set; }
    }
}
