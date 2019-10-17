using System;
using System.ComponentModel.DataAnnotations;
using Spending.Domain.Enum;

namespace Spending.Infrastructure.Data
{
    public class Spending
    {
        public long Id { get; set; }
        public Spender Spender { get; set; }
        public long SpenderId { get; set; }
        public DateTime Date { get; set; }
        public SpendingNature Nature { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public long CurrencyId { get; set; }
        [Required]
        public string Comment { get; set; }
    }
}
