using MonthBudget.Data.Models;

namespace MonthBudget.ServiceContracts
{
    public interface IExpensesService
    {
        Task<Expense> AddExpense(Expense expense);
        Task<bool> RemoveExpense(int expenseId);
        List<Expense> GetExpenses(int userId, DateTime? from = null, DateTime? to = null);
    }
}