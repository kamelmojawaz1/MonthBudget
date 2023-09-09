using System;
using System.Collections.Generic;

namespace MonthBudget.Data.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Avatar { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();

    public virtual ICollection<MonthlyGoal> MonthlyGoals { get; set; } = new List<MonthlyGoal>();

    public virtual ICollection<RecurringExpense> RecurringExpenses { get; set; } = new List<RecurringExpense>();

    public virtual ICollection<RecurringIncome> RecurringIncomes { get; set; } = new List<RecurringIncome>();
}
