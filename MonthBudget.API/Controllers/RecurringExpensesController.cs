using Microsoft.AspNetCore.Mvc;
using MonthBudget.API.Dtos;
using MonthBudget.ServiceContracts;

namespace MonthBudget.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecurringExpensesController : ControllerBase
    {
        private readonly ILogger<RecurringExpensesController> _logger;
        private readonly IRecurringExpensesService _expensesService;

        public RecurringExpensesController(ILogger<RecurringExpensesController> logger, IRecurringExpensesService expensesService)
        {
            _logger = logger;
            _expensesService = expensesService;
        }

        /// <summary>
        /// Retrieves a list of recurring expenses and related monthly expenses for a specific user within a date range.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/RecurringExpenses/GetRecurringExpenses/1/2023-09-01/2023-09-30
        ///
        /// </remarks>
        /// <param name="userId">The ID of the user whose recurring expenses are being retrieved.</param>
        /// <param name="from">The start date of the date range for filtering expenses.</param>
        /// <param name="to">The end date of the date range for filtering expenses.</param>
        /// <returns>A list of recurring expenses and related monthly expenses that match the specified criteria.</returns>
        /// <response code="200">Returns a list of recurring expenses and related monthly expenses that match the criteria.</response>
        /// <response code="400">If the provided parameters are invalid or the request fails.</response>
        [HttpGet("GetRecurringExpenses/{userId}/{from}/{to}")]
        public IActionResult GetRecurringExpenses([FromRoute] int userId = 1, [FromRoute] string from = "01-01-2023", [FromRoute] string to= "01-01-2024")
        {
            try
            {
                var fromDate = Convert.ToDateTime(from);
                var toDate = Convert.ToDateTime(to);
                return Ok(_expensesService.GetRecurringExpenses(userId, fromDate, toDate));
            }
            catch (Exception e)
            {
                _logger.LogError("GetRecurringExpenses request from {userId}: userId = {userId} , from = {from} , to = {to}: error message = {message}", userId, userId, from, to, e.Message);
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Add a new recurring expense with monthly expenses.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/RecurringExpenses/AddRecurringExpense
        ///     {
        ///         "userId": 1,
        ///         "title": "Groceries",
        ///         "note": "Monthly grocery shopping",
        ///         "categoryId": 2,
        ///         "accountId": 1,
        ///         "amount":2000,
        ///         "startDate": "2023-09-08T10:00:00",
        ///         "endDate": "2024-09-08T10:00:00",
        ///     }
        ///
        /// </remarks>
        /// <param name="recurringExpenseDto">Recurring expense object to add</param>
        /// <returns>The newly created expense with related monthly expenses</returns>
        /// <response code="201">Returns the newly created expense with related monthly expenses</response>
        /// <response code="400">If the expense is invalid or validation fails</response>
        [HttpPost("AddRecurringExpense")]
        public async Task<IActionResult> AddRecurringExpense([FromBody] RecurringExpenseDto recurringExpenseDto)
        {
            try
            {
                var result = await _expensesService.AddRecurringExpense(recurringExpenseDto.ConvertToRecurringExpense());

                return CreatedAtAction(nameof(AddRecurringExpense), new { id = result.Item1.Id },result);
            }
            catch (Exception e)
            {
                _logger.LogError("AddRecurringExpense request from {userId}: recurringExpenseDto = {recurringExpenseDto}: error message = {message}",
                    recurringExpenseDto.UserId, recurringExpenseDto.ToString(), e.Message);
                return BadRequest(e.Message + "/n" + e.InnerException);
            }
        }

        /// <summary>
        /// Removes an recurring expense with the specified ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/RecurringExpenses/RemoveRecurringExpense/1
        ///
        /// </remarks>
        /// <param name="recurringExpenseId">The ID of the expense to remove.</param>
        /// <returns>True if the expense was successfully removed; otherwise, false.</returns>
        /// <response code="200">Returns true if the expense was removed successfully.</response>
        /// <response code="400">If the removal request fails or the expense does not exist.</response>
        [HttpDelete("RemoveRecurringExpense/{recurringExpenseId}")]
        public async Task<IActionResult> RemoveRecurringExpense([FromRoute] int recurringExpenseId = 1)
        {
            try
            {
                var removed = await _expensesService.RemoveRecurringExpense(recurringExpenseId);
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
                _logger.LogError("RemoveRecurringExpense recurringExpenseId= {recurringExpenseId} error= {message}", recurringExpenseId, e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}