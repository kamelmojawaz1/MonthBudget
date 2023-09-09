using System;
using System.Collections.Generic;

namespace MonthBudget.Data.Models;

public partial class Income
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Source { get; set; } = null!;

    public string Note { get; set; } = null!;

    public int AccountId { get; set; }

    public DateTime TransactionDate { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public bool? IsActive { get; set; }
}
