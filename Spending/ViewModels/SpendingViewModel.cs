using System;

namespace Spending.ViewModels
{
    public class SpendingViewModel
    {
        public long? Id { get; set; }
        public string SpenderName { get; set; }
        public DateTime? Date { get; set; }
        public string Nature { get; set; }
        public decimal? Amount { get; set; }
        public string CurrencyName { get; set; }
        public string Comment { get; set; }
    }
}
