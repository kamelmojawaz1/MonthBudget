using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MonthBudget.Data.Models;
using System.Text.Json;

namespace MonthBudget.API.Dtos
{
    public class RecurringExpenseDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        [DefaultValue(1)]
        public int UserId { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        [DefaultValue("Rent")]
        public string Title { get; set; } = null!;

        [MaxLength(512)]
        [DefaultValue("Monthly rent")]
        public string Note { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue)]
        [DefaultValue(2)]
        public int CategoryId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [DefaultValue(1)]
        public int AccountId { get; set; }

        [Required]
        [DefaultValue("2023-01-01")]

        public DateTime StartDate { get; set; }

        [Required]
        [DefaultValue("2024-01-01")]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        [DefaultValue(2225)]
        public double Amount { get; set; }

        public RecurringExpense ConvertToRecurringExpense()
        {
            return new RecurringExpense
            {
                UserId = UserId,
                Title = Title,
                Amount = Amount,
                Note = Note,
                AccountId = AccountId,
                StartDate = StartDate,
                EndDate = EndDate,
                CategoryId = CategoryId,
                CreatedOn = DateTime.Now
            };
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize<RecurringExpenseDto>(this);
        }
    }
}
