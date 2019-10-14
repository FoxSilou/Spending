namespace Spending.Infrastructure.Data
{
    public class Spender
    {
        public long Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public Currency Currency { get; set; }
        public long CurrencyId { get; set; }
    }
}
