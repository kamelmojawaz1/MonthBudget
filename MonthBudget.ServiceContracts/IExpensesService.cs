using MonthBudget.Data.Models;

namespace MonthBudget.ServiceContracts
{
    public interface IExpensesService
    {
        public Task<Expense> AddExpense(Expense expense);
        public Task<bool> RemoveExpense(int expenseId);
        public List<Expense> GetExpenses(int userId, DateTime? from = null, DateTime? to = null);
    }
}