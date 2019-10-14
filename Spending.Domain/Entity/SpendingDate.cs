namespace Spending.Domain.Entity
{
    using global::Spending.Domain.Exceptions;
    using System;

    public class SpendingDate
    {
        public DateTime? Date { get; private set; }

        private SpendingDate()
        {
        }

        public static SpendingDate BuildSpendingDate(
            DateTime? value = null)
        {
            return new SpendingDate().SetValue(value);
        }

        public SpendingDate SetValue(DateTime? value = null)
        {
            if (!value.HasValue)
            {
                throw new ValidationException("SpendingDate is not valid");
            }

            Date = value.Value;
            return this;
        }

        // Note : les conditions d'égalité d'une date de dépense ne sont pas très bien définies dans les specs, à revoir éventuellement
        public bool IsEquals(SpendingDate spendingToCompare)
        {
            return spendingToCompare.Date.Value.AddSeconds(-0.1) <= Date && Date <= spendingToCompare.Date.Value.AddSeconds(0.1);
        }

        public void ValidateNew(DateTime now)
        {
            if (this.Date == null // Date should not be null
                || this.Date.Value > now // A spending should not have a date in the future
                || this.Date.Value.AddMonths(3) < now) // A spending should not have a date more than three month old
            {
                throw new ValidationException("SpendingDate is not valid");
            }
        }
    }
}
