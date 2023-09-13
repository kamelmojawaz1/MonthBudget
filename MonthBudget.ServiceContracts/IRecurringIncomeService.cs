using MonthBudget.Data.Models;

namespace MonthBudget.ServiceContracts
{
    public interface IRecurringIncomeService
    {
        Task<(RecurringIncome,List<Income>)> AddRecurringIncome(RecurringIncome recurringIncome);
        Task<bool> RemoveRecurringIncome(int recurringIncomeId);
        (List<RecurringIncome>,List<Income>) GetRecurringIncomes(int userId, DateTime? from = null, DateTime? to = null);
    }
}
