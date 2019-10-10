namespace Spending.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Spending.Domain.Contracts;
    using Spending.Domain.Exceptions;
    using Spending.Domain.ViewModels;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/spendings")]
    public class SpendingController : Controller
    {
        private readonly ISpendingService _spendingService;

        public SpendingController(ISpendingService spendingService)
        {
            _spendingService = spendingService;
        }

        [HttpGet("~/api/spendings/{id}")]
        public ActionResult<SpendingViewModel> Get(long id)
        {
            try
            {
                return Ok(_spendingService.Get(id));
            }
            catch(NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("~/api/spenders/{spenderId}/spendings")]
        public ActionResult<IEnumerable<SpendingViewModel>> GetFromSpender(long spenderId, [FromQuery] string orderBy)
        {
            try
            {
                IEnumerable<SpendingViewModel> rawResult = _spendingService.GetFromSpender(spenderId);
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
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("~/api/spendings")]
        public async Task<ActionResult<SpendingViewModel>> Post([FromBody] CreateSpendingViewModel createViewModel)
        {
            try
            {
                SpendingViewModel insertedSpending = await _spendingService.Create(createViewModel);
                return CreatedAtAction(nameof(Get), new { insertedSpending.Id }, insertedSpending);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
