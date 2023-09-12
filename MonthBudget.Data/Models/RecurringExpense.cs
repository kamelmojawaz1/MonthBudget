using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MonthBudget.Data.Models;

public partial class RecurringExpense
{
    public int Id { get; set; }

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

    public DateTime CreatedOn { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? IsActive { get; set; }

    [Required]
    [Range(1, double.MaxValue)]
    [DefaultValue(2225)]
    public double Amount { get; set; }

}
