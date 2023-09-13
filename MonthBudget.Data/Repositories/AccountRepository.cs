using MonthBudget.Data.Models;

namespace MonthBudget.Data.Repositories
{
    public class AccountRepository
    {
        private readonly MonthBudgetDbContext _dbContext;

        public AccountRepository(MonthBudgetDbContext db)
        {
            _dbContext = db;
        }

        public async Task<Account> AddAccount(Account account)
        {
            await _dbContext.Accounts.AddAsync(account);
            await _dbContext.SaveChangesAsync();
            return account;
        }

        public async Task<bool> RemoveAccount(int accountId)
        {
            var account = _dbContext.Accounts.FirstOrDefault(i => i.Id == accountId);
            if (account == null) return false;

            account.IsActive = false;

            var result = _dbContext.Accounts.Update(account);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public List<Account> GetAll(int userId)
        {
            var accounts = _dbContext.Accounts.Where(i => i.UserId == userId && i.IsActive == true).ToList();
            return accounts ?? new List<Account>();
        }

    }
}
