using System.ComponentModel.DataAnnotations;
using MonthBudget.Data.Models;
using MonthBudget.Data.Repositories;
using MonthBudget.ServiceContracts;

namespace MonthBudget.Services
{
    public class RecurringExpensesService : IRecurringExpensesService
    {
        private readonly RecurringExpensesRepository _recurringExpensesRepository;
        private readonly ExpensesRepository _expensesRepository;

        public RecurringExpensesService(RecurringExpensesRepository recurringExpensesRepository, ExpensesRepository expensesRepository)
        {
            _expensesRepository = expensesRepository;
            _recurringExpensesRepository = recurringExpensesRepository;
        }

        public async Task<(RecurringExpense, List<Expense>)> AddRecurringExpense(RecurringExpense recurringExpense)
        {
            var validationContext = new ValidationContext(recurringExpense, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(recurringExpense, validationContext, validationResults, validateAllProperties: true);

            if (!isValid)
            {
                var errorMessages = validationResults.Select(result => result.ErrorMessage);
                string errorMessage = string.Join("; ", errorMessages);
                throw new ValidationException(errorMessage);
            }
            recurringExpense = await _recurringExpensesRepository.AddRecurringExpense(recurringExpense);

            var monthlyExpenses = new List<Expense>();
            var from = recurringExpense.StartDate;
            var to = recurringExpense.EndDate;
            while (from < to)
            {
                var monthlyExpense = new Expense
                {
                    AccountId = recurringExpense.AccountId,
                    TransactionDate = from,
                    Title = recurringExpense.Title,
                    Note = recurringExpense.Note,
                    CategoryId = recurringExpense.CategoryId,
                    Amount = recurringExpense.Amount,
                    UserId = recurringExpense.UserId,
                    RecurringId = recurringExpense.Id
                };

                monthlyExpense = await _expensesRepository.AddExpense(monthlyExpense);
                monthlyExpenses.Add(monthlyExpense);

                from = from.AddMonths(1);
            }

            return (recurringExpense, monthlyExpenses);
        }


        public (List<RecurringExpense>, List<Expense>) GetRecurringExpenses(int userId, DateTime? from = null, DateTime? to = null)
        {
            if (userId < 0)
                throw new ValidationException($"incorrect userid {userId}");

            var recurringExpenses = from == null || to == null ? _recurringExpensesRepository.GetAll(userId) :
                                                                    _recurringExpensesRepository.GetInRange(userId, (DateTime)from, (DateTime)to);

            var monthlyExpenses = _expensesRepository.GetAll(userId).Where(i => i.RecurringId > 0 &&
                                              recurringExpenses.Any(ri => ri.Id == i.RecurringId) &&
                                                                                i.IsActive == true &&
                                                                                i.TransactionDate >= from &&
                                                                                i.TransactionDate < to).ToList();

            return (recurringExpenses, monthlyExpenses);
        }

        public async Task<bool> RemoveRecurringExpense(int recurringExpenseId)
        {
            if (recurringExpenseId < 0)
                throw new ValidationException($"incorrect recurringExpenseId {recurringExpenseId}");

            var isRecurringRemoved = await _recurringExpensesRepository.RemoveRecurringExpense(recurringExpenseId);

            var isMonthlyRemoved = await _expensesRepository.RemoveExpense(i => i.RecurringId == recurringExpenseId);

            return isRecurringRemoved && isMonthlyRemoved;
        }

    }
}
