using Microsoft.AspNetCore.Mvc;
using MonthBudget.API.Dtos;
using MonthBudget.ServiceContracts;

namespace MonthBudget.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecurringIncomeController : ControllerBase
    {
        private readonly ILogger<RecurringIncomeController> _logger;
        private readonly IRecurringIncomeService _service;

        public RecurringIncomeController(ILogger<RecurringIncomeController> logger, IRecurringIncomeService incomeService)
        {
            _logger = logger;
            _service = incomeService;
        }

        /// <summary>
        /// Adds a new recurring income with monthly income details.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/RecurringIncome/AddRecurringIncome
        ///     {
        ///         "userId": 1,
        ///         "source": "Employment",
        ///         "note": "Monthly paycheck",
        ///         "accountId": 1,
        ///         "amount": 3000,
        ///         "startDate": "2023-09-08T10:00:00",
        ///         "endDate": "2024-09-08T10:00:00",
        ///     }
        ///
        /// </remarks>
        /// <param name="recurringIncomeDto">Recurring income object to add</param>
        /// <returns>The newly created income with related monthly income details</returns>
        /// <response code="201">Returns the newly created income with related monthly income details</response>
        /// <response code="400">If the income is invalid or validation fails</response>
        [HttpPost("AddRecurringIncome")]
        public async Task<IActionResult> AddRecurringIncome([FromBody] RecurringIncomeDto recurringIncomeDto)
        {
            try
            {
                var result = await _service.AddRecurringIncome(recurringIncomeDto.ConvertToRecurringIncome());

                return CreatedAtAction(nameof(AddRecurringIncome), new { id = result.Item1.Id }, new { recurring = result.Item1, income = result.Item2 });
            }
            catch (Exception e)
            {
                _logger.LogError("AddRecurringIncome request from {userId}: recurringIncomeDto = {recurringIncomeDto}: error message = {message}",
                    recurringIncomeDto.UserId, recurringIncomeDto.ToString(), e.Message);
                return BadRequest(e.Message + "\n" + e.InnerException);
            }
        }

        /// <summary>
        /// Retrieves a list of recurring incomes and related monthly incomes for a specific user within a date range.
        /// </summary>
        /// <param name="userId">The ID of the user whose recurring incomes are being retrieved.</param>
        /// <param name="from">The start date of the date range for filtering incomes.</param>
        /// <param name="to">The end date of the date range for filtering incomes.</param>
        /// <returns>A list of recurring incomes and related monthly incomes that match the specified criteria.</returns>
        /// <response code="200">Returns a list of recurring incomes and related monthly incomes that match the criteria.</response>
        /// <response code="400">If the provided parameters are invalid or the request fails.</response>
        [HttpGet("GetRecurringIncome/{userId}/{from}/{to}")]

        public IActionResult GetRecurringIncome([FromRoute] int userId = 1, [FromRoute] string from = "01-01-2023", [FromRoute] string to = "01-01-2024")
        {
            try
            {
                var fromDate = Convert.ToDateTime(from);
                var toDate = Convert.ToDateTime(to); 
                var result = _service.GetRecurringIncomes(userId, fromDate, toDate);

                return Ok(new { recurring = result.Item1, income = result.Item2 });

            }
            catch (Exception e)
            {
                _logger.LogError("GetRecurringIncome request from {userId}: userId = {userId} , from = {from} , to = {to}: error message = {message}", 
                    userId, userId, from, to, e.Message);

                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Removes a recurring income with the specified ID.
        /// </summary>
        /// <param name="incomeId">The ID of the recurring income to remove.</param>
        /// <returns>True if the recurring income was successfully removed; otherwise, false.</returns>
        /// <response code="200">Returns true if the recurring income was removed successfully.</response>
        /// <response code="404">If the removal request fails or the recurring income does not exist.</response>
        [HttpDelete("RemoveRecurringIncome/{incomeId}")]
        public async Task<IActionResult> RemoveRecurringIncome([FromRoute] int incomeId = 1)
        {
            try
            {
                var removed = await _service.RemoveRecurringIncome(incomeId);

                if (removed)
                {
                    return Ok(true);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                _logger.LogError("RemoveRecurringIncome incomeId = {incomeId}: error message = {message}", incomeId, e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
