namespace Spending.Domain.Contracts
{
    public interface ICurrencyRepository
    {
        Entity.Currency Get(long? id);
    }
}
