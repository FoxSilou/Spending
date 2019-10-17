namespace Spending.Domain.Entity
{
    public class Currency
    {
        public long? Id { get; private set; }
        public string Name { get; private set; }

        private Currency()
        {
        }

        public static Currency BuildCurrency(
            long? id = null,
            string name = null)
        {
            return new Currency()
                .SetId(id)
                .SetName(name);
        }

        public Currency SetId(long? id = null)
        {
            Id = id;
            return this;
        }

        public Currency SetName(string name = null)
        {
            Name = name;
            return this;
        }
    }
}
