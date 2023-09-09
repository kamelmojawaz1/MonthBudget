using System;
using System.Collections.Generic;

namespace MonthBudget.Data.Models;

public partial class MonthlyGoal
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int Month { get; set; }

    public int Year { get; set; }

    public int Type { get; set; }

    public double Amount { get; set; }

}
