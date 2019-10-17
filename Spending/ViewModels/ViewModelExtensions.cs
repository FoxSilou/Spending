namespace Spending.ViewModels
{
    public static class ViewModelExtensions
    {
        public static SpendingViewModel ToViewModel(this Domain.Entity.Spending spending)
        {
            return new SpendingViewModel
            {
                Id = spending.Id,
                SpenderName = spending.Spender?.FullName(),
                Date = spending.Date.Date,
                Nature = spending.Nature.ToString(),
                Amount = spending.Amount.Value,
                CurrencyName = spending.Currency?.Name,
                Comment = spending.Comment,
            };
        }
    }
}
