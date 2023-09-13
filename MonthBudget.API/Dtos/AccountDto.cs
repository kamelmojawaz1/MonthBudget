using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MonthBudget.Data.Models;
using System.Text.Json;

namespace MonthBudget.API.Dtos
{
    public class AccountDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        [DefaultValue(1)]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        [DefaultValue("Checking Account")]
        public string AccountName { get; set; } = null!;

        public Account ConvertToAccount()
        {
            return new Account
            {
                UserId = UserId,
                AccountName = AccountName
            };
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize<AccountDto>(this);
        }
    }
}
