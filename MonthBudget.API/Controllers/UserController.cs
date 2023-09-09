using Microsoft.AspNetCore.Mvc;
using MonthBudget.Data.Models;
using MonthBudget.ServiceContracts;

namespace MonthBudget.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger,IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("GetUsers")]
        public List<User> GetUsers()
        {
            var x = _userService.GetUsers();
            return x;
        }

        [HttpGet("GetUser/{userId}/{month}/{year}")]
        public IActionResult GetUser([FromRoute]int userId, [FromRoute] int month, [FromRoute] int year)
        {
            try
            {
                var user = _userService.GetUserWithMonthTransactions(userId, month, year);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}