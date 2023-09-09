using MonthBudget.Data.Models;

namespace MonthBudget.ServiceContracts
{
    public interface IExpensesService
    {
        public Expense AddExpense(string title,string note,int categoryId,int accountId,DateTime transactionDate);
        public bool RemoveExpense(int expenseId);
    }
}