using System.ComponentModel.DataAnnotations;

namespace MonthBudget.Data.Models;

public partial class Account
{
    public int Id { get; set; }


    [Required]
    [Range(1, int.MaxValue)]
    public int UserId { get; set; }

    [Required]
    [MaxLength(100)]
    [MinLength(1)]
    public string AccountName { get; set; } = null!;

    public bool? IsActive { get; set; }
}
