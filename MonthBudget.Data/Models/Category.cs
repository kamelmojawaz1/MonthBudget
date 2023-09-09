namespace MonthBudget.Data.Models;

public partial class Category
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string CategoryName { get; set; } = null!;

}
