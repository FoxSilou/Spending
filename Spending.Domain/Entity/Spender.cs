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
            long? currencyId = null, 
            string currencyName = null)
        {
            return new Spender()
                .SetId(id)
                .SetPerson(firstName: firstName, lastName: lastName)
                .SetCurrency(currencyId: currencyId, currencyName: currencyName);
        }

        public Spender SetId(long? id = null)
        {
            Id = id;
            return this;
        }

        public Spender SetPerson(string firstName = null, string lastName = null)
        {
            Person = Person.BuildPerson(firstName: firstName, lastName: lastName);
            return this;
        }

        public Spender SetCurrency(long? currencyId = null, string currencyName = null)
        {
            Currency = Currency.BuildCurrency(id: currencyId, name: currencyName);
            return this;
        }

        public string FullName()
        {
            return Person.FullName();
        }
    }
}
