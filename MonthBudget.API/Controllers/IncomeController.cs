using Microsoft.AspNetCore.Mvc;
using MonthBudget.API.Dtos;
using MonthBudget.Data.Models;
using MonthBudget.ServiceContracts;

namespace MonthBudget.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncomeController : ControllerBase
    {
        private readonly ILogger<IncomeController> _logger;
        private readonly IIncomeService _incomeService;

        public IncomeController(ILogger<IncomeController> logger, IIncomeService incomeService)
        {
            _logger = logger;
            _incomeService = incomeService;
        }

        /// <summary>
        /// Add a new income record.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Income/AddIncome
        ///     {
        ///         "userId": 1,
        ///         "source": "Salary",
        ///         "note": "Monthly salary",
        ///         "accountId": 3,
        ///         "transactionDate": "2023-09-08T10:00:00"
        ///     }
        ///
        /// </remarks>
        /// <param name="income">Income object to add</param>
        /// <returns>The newly created income record</returns>
        /// <response code="201">Returns the newly created income record</response>
        /// <response code="400">If the income record is invalid or validation fails</response>
        [HttpPost("AddIncome")]
        public async Task<IActionResult> AddIncome([FromBody] IncomeDto incomeDto)
        {
            try
            {
                var createdIncome = await _incomeService.AddIncome(incomeDto.ConvertToIncome());
                return CreatedAtAction(nameof(AddIncome), new { id = createdIncome.Id }, createdIncome);
            }
            catch (Exception e)
            {
                _logger.LogError("AddIncome request from {userId}: incomeDto = {incomDto}: error message = {message}", incomeDto.UserId,incomeDto.ToString(), e.Message);
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Retrieves a list of income records for a specific user within a date range.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Income/GetIncomes/1/2023-09-01/2023-09-30
        ///
        /// </remarks>
        /// <param name="userId">The ID of the user whose income records are being retrieved.</param>
        /// <param name="from">The start date of the date range for filtering income records.</param>
        /// <param name="to">The end date of the date range for filtering income records.</param>
        /// <returns>A list of income records that match the specified criteria.</returns>
        /// <response code="200">Returns a list of income records that match the criteria.</response>
        /// <response code="400">If the provided parameters are invalid or the request fails.</response>
        [HttpGet("GetIncome/{userId}/{from}/{to}")]
        public IActionResult GetIncome([FromRoute] int userId = 1, [FromRoute] string from = "01-01-2023", [FromRoute] string to = "01-01-2024")
        {
            try
            {
                var fromDate = Convert.ToDateTime(from);
                var toDate = Convert.ToDateTime(to);
                var incomes = _incomeService.GetIncomes(userId, fromDate, toDate);
                return Ok(incomes);
            }
            catch (Exception e)
            {
                _logger.LogError("GetIncome request from {userId}: userId = {userId} , from = {from} , to = {to}: error message = {message}", userId, userId, from, to, e.Message);
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Removes an income record with the specified ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/Income/RemoveIncome/123
        ///
        /// </remarks>
        /// <param name="incomeId">The ID of the income record to remove.</param>
        /// <returns>True if the income record was successfully removed; otherwise, false.</returns>
        /// <response code="200">Returns true if the income record was removed successfully.</response>
        /// <response code="400">If the removal request fails or the income record does not exist.</response>
        [HttpDelete("RemoveIncome/{incomeId}")]
        public async Task<IActionResult> RemoveIncome([FromRoute] int incomeId = 1)
        {
            try
            {
                var removed = await _incomeService.RemoveIncome(incomeId);
                if (removed)
                {
                    return Ok(true);
                }
                else
                {
                    return NotFound(); // Return 404 if the income record was not found
                }
            }
            catch (Exception e)
            {
                _logger.LogError("RemoveIncome request= {incomeId} error= {message}", incomeId, e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
