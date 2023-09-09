using Microsoft.EntityFrameworkCore;
using MonthBudget.Data.Models;

namespace MonthBudget.Data.Repositories
{
    public class UserRepository
    {
        private readonly MonthBudgetDbContext _dbContext;

        public UserRepository(MonthBudgetDbContext db)
        {
            _dbContext = db;
        }

        public List<User> GetUsers()
        {
            return _dbContext.Users.ToList();
        }

        public User? GetById(int id)
        {
            return _dbContext.Users
                    .FirstOrDefault(u => u.Id == id);
        }

        public User? GetByIdWithAllTransactions(int id)
        {
            return _dbContext.Users
                    .Include(u => u.Accounts)
                    .Include(u => u.Incomes)
                    .Include(u => u.Expenses)
                    .Include(u => u.RecurringExpenses)
                    .Include(u => u.RecurringIncomes)
                    .Include(u => u.MonthlyGoals)
                    .Include(u => u.Categories)
                    .FirstOrDefault(u => u.Id == id);
        }

        public User? GetByIdWithRangeTransactions(int id, DateTime fromDate,DateTime toDate)
        {
            return _dbContext.Users
                    .Include(u => u.Accounts)
                    .Include(u => u.Incomes.Where(e => e.TransactionDate >= fromDate && e.TransactionDate <= toDate))
                    .Include(u => u.Expenses.Where(e=>e.TransactionDate>=fromDate && e.TransactionDate <= toDate))
                    .Include(u=> u.MonthlyGoals)
                    .Include(u => u.RecurringExpenses)
                    .Include(u => u.RecurringIncomes)
                    .Include(u => u.Categories)
                    .FirstOrDefault(u => u.Id == id);
        }
    }
}
