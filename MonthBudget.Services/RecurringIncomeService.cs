using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using MonthBudget.Data.Models;
using MonthBudget.Data.Repositories;
using MonthBudget.ServiceContracts;

namespace MonthBudget.Services
{
    public class RecurringIncomeService : IRecurringIncomeService
    {
        private readonly RecurringIncomeRepository _recurringIncomeRepository;
        private readonly IncomeRepository _incomeRepository;

        public RecurringIncomeService(RecurringIncomeRepository recurringIncomeRepository, IncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
            _recurringIncomeRepository = recurringIncomeRepository;
        }

        public async Task<(RecurringIncome, List<Income>)> AddRecurringIncome(RecurringIncome recurringIncome)
        {
            var validationContext = new ValidationContext(recurringIncome, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(recurringIncome, validationContext, validationResults, validateAllProperties: true);

            if (!isValid)
            {
                var errorMessages = validationResults.Select(result => result.ErrorMessage);
                string errorMessage = string.Join("; ", errorMessages);
                throw new ValidationException(errorMessage);
            }
            recurringIncome = await _recurringIncomeRepository.AddRecurringIncome(recurringIncome);

            var monthlyIncomes = new List<Income>();
            var from = recurringIncome.StartDate;
            var to = recurringIncome.EndDate;
            while (from < to)
            {
                var monthlyIncome = new Income
                {
                    AccountId = recurringIncome.AccountId,
                    TransactionDate = from,
                    Source = recurringIncome.Source,
                    Note = recurringIncome.Note,
                    Amount = recurringIncome.Amount,
                    UserId = recurringIncome.UserId,
                    RecurringId = recurringIncome.Id
                };

                var income = await _incomeRepository.AddIncome(monthlyIncome);
                monthlyIncomes.Add(income);
                from = from.AddMonths(1);
            }

            return (recurringIncome, monthlyIncomes);
        }

        public (List<RecurringIncome>, List<Income>) GetRecurringIncomes(int userId, DateTime? from = null, DateTime? to = null)
        {
            if (userId < 0)
                throw new ValidationException($"incorrect userid {userId}");

            var recurringIncomes = from == null || to == null ? _recurringIncomeRepository.GetAll(userId) :
                                                                    _recurringIncomeRepository.GetInRange(userId, (DateTime)from, (DateTime)to);

            var monthlyIncomes = _incomeRepository.GetAll(userId).Where(i => i.RecurringId > 0 && 
                                                          recurringIncomes.Any(ri=>ri.Id == i.RecurringId) && 
                                                                                            i.IsActive == true && 
                                                                                            i.TransactionDate>=from && 
                                                                                            i.TransactionDate<to).ToList();

            return (recurringIncomes, monthlyIncomes);
        }

        public async Task<bool> RemoveIncome(int incomeId)
        {
            if (incomeId < 0)
                throw new ValidationException($"incorrect incomeId {incomeId}");

            return await _incomeRepository.RemoveIncome(incomeId);
        }

        public async Task<bool> RemoveRecurringIncome(int recurringIncomeId)
        {
            if (recurringIncomeId < 0)
                throw new ValidationException($"incorrect recurringIncomeId {recurringIncomeId}");

            var isRecurringRemoved =  await _recurringIncomeRepository.RemoveRecurringIncome(recurringIncomeId);
            
            var isMonthlyRemoved = await _incomeRepository.RemoveIncome(i=>i.RecurringId == recurringIncomeId);

            return isRecurringRemoved && isMonthlyRemoved;

        }
    }
}
