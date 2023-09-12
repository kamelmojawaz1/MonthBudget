using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MonthBudget.Data.Models;

public partial class Expense
{
    public int Id { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    [DefaultValue(1)]
    public int UserId { get; set; }

    [Required]
    [MaxLength(100)]
    [MinLength(1)]
    [DefaultValue("Aria Market")]
    public string Title { get; set; } = null!;

    [MaxLength(512)]
    [DefaultValue("Month Grocery Shopping with Lolo")]
    public string Note { get; set; } = null!;

    [Required]
    [Range(1, int.MaxValue)]
    [DefaultValue(2)]
    public int CategoryId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    [DefaultValue(1)]
    public int AccountId { get; set; }

    public int RecurringId { get; set; }

    [Required]
    [Range(1, double.MaxValue)]
    [DefaultValue(100.25)]
    public double Amount { get; set; }

    [Required]
    public DateTime TransactionDate { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public bool? IsActive { get; set; }
}
