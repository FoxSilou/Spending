namespace Spending.Domain.ViewModels
{
    public static class ViewModelExtensions
    {
        public static SpendingViewModel ToViewModel(this Models.SpendingDto spendingDto, Models.SpenderDto spender, Models.CurrencyDto currency)
        {

            Entity.Spending spending = Entity.Spending.BuildSpending(
                amount: spendingDto.Amount,
                comment: spendingDto.Comment,
                currencyId: currency?.Id,
                currencyName: currency?.Name,
                spenderId: spender?.Id,
                firstName: spender?.FirstName,
                lastName: spender?.LastName,
                spenderCurrencyId: spender.CurrencyId,
                date: spendingDto.Date,
                nature: spendingDto.Nature);

            return new SpendingViewModel
            {
                Id = spending.Id,
                SpenderName = spending.Spender.FullName(),
                Date = spending.Date.Date,
                Nature = spending.Nature.ToString(),
                Amount = spending.Amount.Value,
                CurrencyName = currency?.Name,
                Comment = spending.Comment,
            };
        }

        public static Models.SpendingDto ToDto(this CreateSpendingViewModel spending)
        {
            return new Models.SpendingDto
            {
                SpenderId = spending.SpenderId,
                Date = spending.Date,
                Nature = spending.Nature,
                Amount = spending.Amount,
                CurrencyId = spending.CurrencyId,
                Comment = spending.Comment,
            };
        }
    }
}
