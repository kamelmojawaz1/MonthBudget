using Microsoft.AspNetCore.Mvc;
using MonthBudget.Data.Models;
using MonthBudget.ServiceContracts;

namespace MonthBudget.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IExpensesService _expensesService;

        public ExpensesController(ILogger<UserController> logger, IExpensesService expensesService)
        {
            _logger = logger;
            _expensesService = expensesService;
        }

        /// <summary>
        /// Retrieves a list of expenses for a specific user within a date range.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/GetExpenses/1/2023-09-01/2023-09-30
        ///
        /// </remarks>
        /// <param name="userId">The ID of the user whose expenses are being retrieved.</param>
        /// <param name="from">The start date of the date range for filtering expenses.</param>
        /// <param name="to">The end date of the date range for filtering expenses.</param>
        /// <returns>A list of expenses that match the specified criteria.</returns>
        /// <response code="200">Returns a list of expenses that match the criteria.</response>
        /// <response code="400">If the provided parameters are invalid or the request fails.</response>
        [HttpGet("GetExpenses/{userId}/{from}/{to}")]
        public IActionResult GetExpenses([FromRoute] int userId = 1, [FromRoute] string from = "01-01-2023", [FromRoute] string to= "01-01-2024")
        {
            try
            {
                var fromDate = Convert.ToDateTime(from);
                var toDate = Convert.ToDateTime(to);
                return Ok(_expensesService.GetExpenses(userId, fromDate, toDate));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Add a new expense.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/AddExpense
        ///     {
        ///         "userId": 1,
        ///         "title": "Groceries",
        ///         "note": "Monthly grocery shopping",
        ///         "categoryId": 2,
        ///         "accountId": 3,
        ///         "transactionDate": "2023-09-08T10:00:00"
        ///     }
        ///
        /// </remarks>
        /// <param name="expense">Expense object to add</param>
        /// <returns>The newly created expense</returns>
        /// <response code="201">Returns the newly created expense</response>
        /// <response code="400">If the expense is invalid or validation fails</response>
        [HttpPost("AddExpense")]
        public async Task<IActionResult> AddExpense([FromBody] Expense expense)
        {
            try
            {
                var createdExpense = await _expensesService.AddExpense(expense);
                return CreatedAtAction(nameof(AddExpense), new { id = createdExpense.Id }, createdExpense);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + "/n" + e.InnerException);
            }
        }

        /// <summary>
        /// Removes an expense with the specified ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/RemoveExpense/123
        ///
        /// </remarks>
        /// <param name="expenseId">The ID of the expense to remove.</param>
        /// <returns>True if the expense was successfully removed; otherwise, false.</returns>
        /// <response code="200">Returns true if the expense was removed successfully.</response>
        /// <response code="400">If the removal request fails or the expense does not exist.</response>
        [HttpDelete("RemoveExpense/{expenseId}")]
        public async Task<IActionResult> RemoveExpense([FromRoute] int expenseId = 1)
        {
            try
            {
                return Ok(await _expensesService.RemoveExpense(expenseId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}