using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MonthBudget.Data.Models;

public partial class RecurringIncome
{
    public int Id { get; set; }

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

    public DateTime CreatedOn { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? IsActive { get; set; }

    [Required]
    [Range(1, double.MaxValue)]
    [DefaultValue(2225)]
    public double Amount { get; set; }

}
