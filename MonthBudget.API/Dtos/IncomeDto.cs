using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MonthBudget.Data.Models;

namespace MonthBudget.API.Dtos
{
    public class IncomeDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        [DefaultValue(1)]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        [DefaultValue("Employment")]
        public string Source { get; set; } = null!;

        [MaxLength(512)]
        [DefaultValue("Paycheck 1")]
        public string Note { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue)]
        [DefaultValue(1)]
        public int AccountId { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        [DefaultValue(1500)]
        public double Amount { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        public Income ConvertToIncome()
        {
            return new Income
            {
                UserId = UserId,
                Source = Source,
                Amount = Amount,
                Note = Note,
                AccountId = AccountId,
                TransactionDate = TransactionDate
            };
        }
    }
}
