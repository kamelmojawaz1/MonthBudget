using Microsoft.EntityFrameworkCore;
using MonthBudget.Data.Models;

namespace MonthBudget.Data.Repositories
{
    public class ExpensesRepository
    {
        private readonly MonthBudgetDbContext _dbContext;

        public ExpensesRepository(MonthBudgetDbContext db)
        {
            _dbContext = db;
        }

        public async Task<EntityState> AddExpense(Expense expense)
        {
            var x = await _dbContext.Expenses.AddAsync(expense);
            await _dbContext.SaveChangesAsync();
            return x.State;
        }
    }
}
