using MonthBudget.Data.Models;

namespace MonthBudget.ServiceContracts
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUser(int userId);
        User GetUserWithAllTransactions(int userId);
        User GetUserWithMonthTransactions(int userId,int month,int year);
    }
}