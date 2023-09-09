namespace MonthBudget.Data.Models;

public partial class Account
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string AccountName { get; set; } = null!;
}
