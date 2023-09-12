using MonthBudget.Data.Models;

namespace MonthBudget.Data.Repositories
{
    public class RecurringExpensesRepository
    {
        private readonly MonthBudgetDbContext _dbContext;

        public RecurringExpensesRepository(MonthBudgetDbContext db)
        {
            _dbContext = db;
        }

        public async Task<RecurringExpense> AddRecurringExpense(RecurringExpense expense)
        {
            await _dbContext.RecurringExpenses.AddAsync(expense);
            await _dbContext.SaveChangesAsync();
            return expense;
        }

        public async Task<bool> RemoveRecurringExpense(int expenseId)
        {
            var expense = _dbContext.RecurringExpenses.FirstOrDefault(e => e.Id == expenseId);
            if (expense == null) return false;

            expense.IsActive = false;
            //expense.UpdatedOn = DateTime.Now;/**this assumes db and server are in the same timezone**/

            var result = _dbContext.RecurringExpenses.Update(expense);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public RecurringExpense? GetById(int expenseId)
        {
            return _dbContext.RecurringExpenses.FirstOrDefault(e => e.Id == expenseId && e.IsActive == true);
        }

        public List<RecurringExpense> GetAll(int userId)
        {
            var expenses = _dbContext.RecurringExpenses.Where(e => e.UserId == userId && e.IsActive == true).ToList();
            return expenses ?? new List<RecurringExpense>();
        }

        public List<RecurringExpense> GetInRange(int userId, DateTime from, DateTime to)
        {
            var expenses = _dbContext.RecurringExpenses.Where(e => e.UserId == userId && e.IsActive == true && e.StartDate >= from && e.EndDate <= to).ToList();
            return expenses ?? new List<RecurringExpense>();
        }
    }
}
