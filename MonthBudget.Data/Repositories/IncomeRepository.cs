using MonthBudget.Data.Models;

namespace MonthBudget.Data.Repositories
{
    public class IncomeRepository
    {
        private readonly MonthBudgetDbContext _dbContext;

        public IncomeRepository(MonthBudgetDbContext db)
        {
            _dbContext = db;
        }

        public async Task<Income> AddIncome(Income income)
        {
            await _dbContext.Incomes.AddAsync(income);
            await _dbContext.SaveChangesAsync();
            return income;
        }

        public async Task<bool> RemoveIncome(int incomeId)
        {
            var income = _dbContext.Incomes.FirstOrDefault(i => i.Id == incomeId);
            if (income == null) return false;

            income.IsActive = false;
            income.UpdatedOn = DateTime.Now;/**this assumes db and server are in the same timezone**/

            var result = _dbContext.Incomes.Update(income);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public List<Income> GetAll(int userId)
        {
            var incomes = _dbContext.Incomes.Where(i => i.UserId == userId && i.IsActive == true).ToList();
            return incomes ?? new List<Income>();
        }

        public List<Income> GetInRange(int userId, DateTime from, DateTime to)
        {
            var incomes = _dbContext.Incomes.Where(i => i.UserId == userId && i.IsActive == true && i.TransactionDate >= from && i.TransactionDate <= to).ToList();
            return incomes ?? new List<Income>();
        }
    }
}
