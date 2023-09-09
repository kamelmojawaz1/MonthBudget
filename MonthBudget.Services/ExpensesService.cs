using MonthBudget.Data.Models;
using MonthBudget.Data.Repositories;
using MonthBudget.ServiceContracts;
using System.ComponentModel.DataAnnotations;

namespace MonthBudget.Services
{
    public class ExpensesService : IExpensesService
    {
        private readonly ExpensesRepository _expensesRepository;

        public ExpensesService(ExpensesRepository expensesRepository)
        {
            _expensesRepository = expensesRepository;
        }

        public async Task<Expense> AddExpense(Expense expense)
        {
            // Validate the expense object
            var validationContext = new ValidationContext(expense, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(expense, validationContext, validationResults, validateAllProperties: true);

            if (!isValid)
            {
                // If there are validation errors, collect the error messages and throw a ValidationException
                var errorMessages = validationResults.Select(result => result.ErrorMessage);
                string errorMessage = string.Join("; ", errorMessages);
                throw new ValidationException(errorMessage);
            }
            return await _expensesRepository.AddExpense(expense); ;
        }

        public List<Expense> GetExpenses(int userId, DateTime? from = null, DateTime? to = null)
        {
            if (userId < 0)
                throw new ValidationException($"incorrect userid {userId}");

            return from == null || to == null ? _expensesRepository.GetAll(userId) :
                                                _expensesRepository.GetAll(userId);
        }

        public async Task<bool> RemoveExpense(int expenseId)
        {
            if (expenseId < 0)
                throw new ValidationException($"incorrect expenseId {expenseId}");

            return await _expensesRepository.RemoveExpense(expenseId);
        }

    }
}