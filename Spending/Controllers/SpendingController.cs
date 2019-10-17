namespace Spending.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Spending.Domain.Contracts;
    using Spending.Domain.Exceptions;
    using Spending.Middlewares;
    using Spending.ViewModels;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/spendings")]
    public class SpendingController : Controller
    {
        private readonly ISpendingService _spendingService;
        private readonly ISpendingRepository _spendingRepository;
        private readonly ISpenderRepository _spenderRepository;
        private readonly ICurrencyRepository _currencyRepository;

        public SpendingController(ISpendingService spendingService, ISpendingRepository spendingRepository, ISpenderRepository spenderRepository, ICurrencyRepository currencyRepository)
        {
            _spendingService = spendingService;
            _spendingRepository = spendingRepository;
            _spenderRepository = spenderRepository;
            _currencyRepository = currencyRepository;
        }

        [HttpGet("~/api/spendings/{id}")]
        public ActionResult<SpendingViewModel> Get(long id)
        {
            Domain.Entity.Spending spending = _spendingRepository.Get(id);
            if (spending == null)
            {
                return NotFound("Spending not found");
            }

            return Ok(spending.ToViewModel());
        }

        [HttpGet("~/api/spenders/{spenderId}/spendings")]
        public ActionResult<IEnumerable<SpendingViewModel>> GetFromSpender(long spenderId, [FromQuery] string orderBy)
        {
            Domain.Entity.Spender spender = _spenderRepository.Get(spenderId);
            if (spender == null)
            {
                return NotFound("Spender not found");
            }

            IList<Domain.Entity.Spending> spendings = _spendingRepository.GetFromSpender(spenderId);
            IList<SpendingViewModel> rawResult = spendings.Select(sp => sp.ToViewModel()).ToList();

            switch (orderBy)
            {
                case (nameof(SpendingViewModel.Amount)):
                    return Ok(rawResult.OrderBy(sp => sp.Amount));
                case (nameof(SpendingViewModel.Date)):
                    return Ok(rawResult.OrderBy(sp => sp.Date));
                default:
                    return Ok(rawResult);
            }
        }

        [HttpPost("~/api/spendings")]
        public async Task<ActionResult<SpendingViewModel>> Post([FromBody] CreateSpendingViewModel createViewModel)
        {
            Domain.Entity.Currency currency = _currencyRepository.Get(createViewModel.CurrencyId);
            if (currency == null)
            {
                return NotFound("Currency not found");
            }

            Domain.Entity.Spender spender = _spenderRepository.Get(createViewModel.SpenderId);
            if (spender == null)
            {
                return NotFound("Spender not found");
            }

            Domain.Entity.Spending spendingToCreate = Domain.Entity.Spending.BuildSpending(
                amount: createViewModel.Amount,
                comment: createViewModel.Comment,
                currency: currency,
                spender: spender,
                date: createViewModel.Date?.Date,
                nature: createViewModel.Nature);

            _spendingService.ValidateNewSpending(spendingToCreate);

            Domain.Entity.Spending insertedSpending = await _spendingRepository.Insert(spendingToCreate);
            return CreatedAtAction(nameof(Get), new { insertedSpending.Id }, insertedSpending.ToViewModel());
        }
    }
}
