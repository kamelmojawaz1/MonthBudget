using MonthBudget.Data.Models;

namespace MonthBudget.ServiceContracts
{
    public interface IAccountService
    {
        Task<Account> AddAccount(Account account);
        Task<bool> RemoveAccount(int accountId);
        List<Account> GetAccounts(int userId);
    }
}
