using Microsoft.AspNetCore.Mvc;
using MonthBudget.API.Dtos;
using MonthBudget.ServiceContracts;

namespace MonthBudget.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        /// <summary>
        /// Adds a new account.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Account/AddAccount
        ///     {
        ///         "userId": 1,
        ///         "accountName": "Savings Account",
        ///         "isActive": true
        ///     }
        ///
        /// </remarks>
        /// <param name="account">Account object to add</param>
        /// <returns>The newly created account</returns>
        /// <response code="201">Returns the newly created account</response>
        /// <response code="400">If the account is invalid or validation fails</response>
        [HttpPost("AddAccount")]
        public async Task<IActionResult> AddAccount([FromBody] AccountDto accountDto)
        {
            try
            {
                var createdAccount = await _accountService.AddAccount(accountDto.ConvertToAccount());
                return CreatedAtAction(nameof(AddAccount), new { id = createdAccount.Id }, createdAccount);
            }
            catch (Exception e)
            {
                _logger.LogError("AddAccount request from {userId}: accountDto = {accountDto}: error message = {message}",
                        accountDto.UserId, accountDto.ToString(), e.Message);
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Removes an account with the specified ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/Account/RemoveAccount/123
        ///
        /// </remarks>
        /// <param name="accountId">The ID of the account to remove.</param>
        /// <returns>True if the account was successfully removed; otherwise, false.</returns>
        /// <response code="200">Returns true if the account was removed successfully.</response>
        /// <response code="404">If the account with the specified ID is not found.</response>
        /// <response code="400">If the removal request fails.</response>
        [HttpDelete("RemoveAccount/{accountId}")]
        public async Task<IActionResult> RemoveAccount([FromRoute] int accountId = 1)
        {
            try
            {
                var removed = await _accountService.RemoveAccount(accountId);
                if (removed)
                {
                    return Ok(true);
                }
                else
                {
                    return NotFound(); // Account with the specified ID not found
                }
            }
            catch (Exception e)
            {
                _logger.LogError("RemoveAccount accountId= {accountId} error= {message}", accountId, e.Message);
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Retrieves a list of accounts for a specific user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Account/GetAccounts/1
        ///
        /// </remarks>
        /// <param name="userId">The ID of the user whose accounts are being retrieved.</param>
        /// <returns>A list of accounts for the specified user.</returns>
        /// <response code="200">Returns a list of accounts for the specified user.</response>
        /// <response code="400">If the provided parameters are invalid or the request fails.</response>
        [HttpGet("GetAccounts/{userId}")]
        public IActionResult GetAccounts([FromRoute] int userId = 1)
        {
            try
            {
                var accounts = _accountService.GetAccounts(userId);
                return Ok(accounts);
            }
            catch (Exception e)
            {
                _logger.LogError("GetAccounts userId= {userId} error= {message}", userId, e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
