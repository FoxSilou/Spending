namespace Spending.Domain.Entity
{
    public class Person
    {
        public string LastName { get; private set; }
        public string FirstName { get; private set; }

        private Person()
        {
        }

        public static Person BuildPerson(
            string lastName = null,
            string firstName = null)
        {
            return new Person()
                .SetLastName(lastName)
                .SetFirstName(firstName);
        }

        public Person SetLastName(string lastName = null)
        {
            LastName = lastName;
            return this;
        }

        public Person SetFirstName(string firstName = null)
        {
            FirstName = firstName;
            return this;
        }

        public string FullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
