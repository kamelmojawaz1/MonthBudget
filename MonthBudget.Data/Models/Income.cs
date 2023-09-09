using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MonthBudget.Data.Models;

public partial class Income
{
    public int Id { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int UserId { get; set; }

    [Required]
    [MaxLength(100)]
    [MinLength(1)]
    public string Source { get; set; } = null!;

    [MaxLength(512)]
    public string Note { get; set; } = null!;

    [Required]
    [Range(1, int.MaxValue)]
    public int AccountId { get; set; }

    [Required]
    [Range(1, double.MaxValue)]
    public double Amount { get; set; }

    [Required]
    public DateTime TransactionDate { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public bool? IsActive { get; set; }
}
