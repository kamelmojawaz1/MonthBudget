using System;
using System.Collections.Generic;

namespace MonthBudget.Data.Models;

public partial class RecurringExpense
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; } = null!;

    public string Note { get; set; } = null!;

    public int CategoryId { get; set; }

    public int AccountId { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? IsActive { get; set; }

}
