namespace Spending.Domain.Contracts
{
    public interface ISpendingService
    {
        void ValidateNewSpending(Entity.Spending spendingToValidate);
    }
}
