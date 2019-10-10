namespace Spending.Domain.Models
{
    using System;

    public class SpenderDto
    {
        public long? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? CurrencyId { get; set; }
    }
}
