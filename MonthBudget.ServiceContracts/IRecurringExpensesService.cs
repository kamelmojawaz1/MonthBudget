using MonthBudget.Data.Models;

namespace MonthBudget.ServiceContracts
{
    public interface IRecurringExpensesService
    {
        Task<(RecurringExpense, List<Expense>)> AddRecurringExpense(RecurringExpense recurringExpense);
        Task<bool> RemoveRecurringExpense(int recurringExpenseId);
        (List<RecurringExpense>, List<Expense>) GetRecurringExpenses(int userId, DateTime? from = null, DateTime? to = null);
    }
}