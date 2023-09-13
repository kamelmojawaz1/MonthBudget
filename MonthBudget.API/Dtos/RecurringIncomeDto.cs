using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MonthBudget.Data.Models;
using System.Text.Json;

namespace MonthBudget.API.Dtos
{
    public class RecurringIncomeDto
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
        [DefaultValue("Monthly paycheck")]
        public string Note { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue)]
        [DefaultValue(1)]
        public int AccountId { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        [DefaultValue("2023-01-01")]
        public DateTime StartDate { get; set; }

        [Required]
        [DefaultValue("2024-01-01")]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        [DefaultValue(3000)]
        public double Amount { get; set; }

        public RecurringIncome ConvertToRecurringIncome()
        {
            return new RecurringIncome
            {
                UserId = UserId,
                Source = Source,
                Amount = Amount,
                Note = Note,
                AccountId = AccountId,
                StartDate = StartDate,
                EndDate = EndDate,
                CreatedOn = DateTime.Now
            };
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize<RecurringIncomeDto>(this);
        }
    }
}
