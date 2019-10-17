namespace Spending.Domain.Tests
{
    using Spending.Domain.Entity;
    using Spending.Domain.Enum;
    using System;

    public static class TestHelpers
    {
        private static readonly Random _random = new Random();

        public static Spending BuildRandomValidPending()
        {
            Currency currency = Currency.BuildCurrency(id: _random.Next(), name: GetRandomString());

            return Spending.BuildSpending(
                amount: GetRandomId(),
                comment: GetRandomString(),
                currency: currency,
                spender: Spender.BuildSpender(
                    id: _random.Next(),
                    currency: currency,
                    firstName: GetRandomString(),
                    lastName: GetRandomString()),
                date: GetRandomDate(dayStart: -90, dayEnd: 0),
                nature: GetRandomNature());
        }

        public static Currency BuildRandomValidCurrency()
        {
            return Currency.BuildCurrency(id: _random.Next(), name: GetRandomString());
        }

        public static SpendingNature GetRandomNature()
        {
            return (SpendingNature)Enum.ToObject(typeof(SpendingNature), _random.Next(0, 2));
        }

        public static string GetRandomString()
        {
            return Guid.NewGuid().ToString();
        }

        public static long GetRandomId()
        {
            return _random.Next();
        }

        public static long GetRandomQuantity()
        {
            return _random.Next();
        }

        public static DateTime GetRandomDate(int dayStart, int dayEnd)
        {
            return DateTime.Now.AddDays(_random.Next(dayStart, dayEnd));
        }
    }
}
