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

        public async Task<Expense> AddExpense(Expense expense)
        {
            await _dbContext.Expenses.AddAsync(expense);
            await _dbContext.SaveChangesAsync();
            return expense;
        }

        public async Task<bool> RemoveExpense(int expenseId)
        {
            var expense = _dbContext.Expenses.FirstOrDefault(e => e.Id == expenseId);
            if (expense == null) return false;

            expense.IsActive = false;

            var result = _dbContext.Expenses.Update(expense);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public Expense? GetById(int expenseId)
        {
            return _dbContext.Expenses.FirstOrDefault(e => e.Id == expenseId && e.IsActive == true);
        }

        public List<Expense> GetAll(int userId)
        {
            var expenses = _dbContext.Expenses.Where(e => e.UserId == userId && e.IsActive == true).ToList();
            return expenses ?? new List<Expense>();
        }

        public List<Expense> GetInRange(int userId, DateTime from, DateTime to)
        {
            var expenses = _dbContext.Expenses.Where(e => e.UserId == userId && e.IsActive == true && e.TransactionDate >= from && e.TransactionDate <= to).ToList();
            return expenses ?? new List<Expense>();
        }
    }
}
