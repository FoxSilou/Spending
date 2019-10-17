using Spending.Domain.ValueObject;

namespace Spending.Domain.Entity
{
    public class Spender
    {
        public long? Id { get; private set; }
        public Person Person { get; private set; }
        public Currency Currency { get; private set; }

        private Spender()
        {
        }

        public static Spender BuildSpender(
            long? id = null,
            string lastName = null,
            string firstName = null,
            Currency currency = null)
        {
            return new Spender()
                .SetId(id)
                .SetPerson(firstName: firstName, lastName: lastName)
                .SetCurrency(currency);
        }

        public Spender SetId(long? id = null)
        {
            Id = id;
            return this;
        }

        public Spender SetPerson(string firstName = null, string lastName = null)
        {
            Person = new Person(firstName: firstName, lastName: lastName);
            return this;
        }

        public Spender SetCurrency(Currency currency)
        {
            Currency = currency;
            return this;
        }

        public string FullName()
        {
            return Person.FullName();
        }
    }
}
