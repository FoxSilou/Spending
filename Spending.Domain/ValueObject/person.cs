namespace Spending.Domain.ValueObject
{
    public struct Person
    {
        public string LastName { get; private set; }
        public string FirstName { get; private set; }

        public Person(string lastName = null, string firstName = null)
        {
            LastName = lastName;
            FirstName = firstName;
        }

        public string FullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
