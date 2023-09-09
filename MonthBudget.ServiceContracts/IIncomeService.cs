using MonthBudget.Data.Models;

namespace MonthBudget.ServiceContracts
{
    public interface IIncomeService
    {
        Task<Income> AddIncome(Income income);
        Task<bool> RemoveIncome(int incomeId);
        List<Income> GetIncomes(int userId, DateTime? from = null, DateTime? to = null);
    }
}
