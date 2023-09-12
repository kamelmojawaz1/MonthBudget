using MonthBudget.Data.Models;

namespace MonthBudget.Data.Repositories
{
    public class RecurringIncomeRepository
    {
        private readonly MonthBudgetDbContext _dbContext;

        public RecurringIncomeRepository(MonthBudgetDbContext db)
        {
            _dbContext = db;
        }

        public async Task<RecurringIncome> AddRecurringIncome(RecurringIncome income)
        {
            await _dbContext.RecurringIncomes.AddAsync(income);
            await _dbContext.SaveChangesAsync();
            return income;
        }

        public async Task<bool> RemoveRecurringIncome(int incomeId)
        {
            var income = _dbContext.RecurringIncomes.FirstOrDefault(e => e.Id == incomeId);
            if (income == null) return false;

            income.IsActive = false;
            //expense.UpdatedOn = DateTime.Now;/**this assumes db and server are in the same timezone**/

            var result = _dbContext.RecurringIncomes.Update(income);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public RecurringIncome? GetById(int incomeId)
        {
            return _dbContext.RecurringIncomes.FirstOrDefault(e => e.Id == incomeId && e.IsActive == true);
        }

        public List<RecurringIncome> GetAll(int userId)
        {
            var incomes = _dbContext.RecurringIncomes.Where(e => e.UserId == userId && e.IsActive == true).ToList();
            return incomes ?? new List<RecurringIncome>();
        }

        public List<RecurringIncome> GetInRange(int userId, DateTime from, DateTime to)
        {
            var expenses = _dbContext.RecurringIncomes.Where(e => e.UserId == userId && e.IsActive == true && e.StartDate >= from && e.EndDate <= to).ToList();
            return expenses ?? new List<RecurringIncome>();
        }
    }
}
