using System.ComponentModel.DataAnnotations;
using MonthBudget.Data.Models;
using MonthBudget.Data.Repositories;
using MonthBudget.ServiceContracts;

namespace MonthBudget.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly IncomeRepository _incomeRepository;

        public IncomeService(IncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public async Task<Income> AddIncome(Income income)
        {
            // Validate the income object
            var validationContext = new ValidationContext(income, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(income, validationContext, validationResults, validateAllProperties: true);

            if (!isValid)
            {
                // If there are validation errors, collect the error messages and throw a ValidationException
                var errorMessages = validationResults.Select(result => result.ErrorMessage);
                string errorMessage = string.Join("; ", errorMessages);
                throw new ValidationException(errorMessage);
            }
            return await _incomeRepository.AddIncome(income);
        }

        public List<Income> GetIncomes(int userId, DateTime? from = null, DateTime? to = null)
        {
            if (userId < 0)
                throw new ValidationException($"incorrect userid {userId}");

            return from == null || to == null ? _incomeRepository.GetAll(userId) :
                                                _incomeRepository.GetInRange(userId, from.Value, to.Value);
        }

        public async Task<bool> RemoveIncome(int incomeId)
        {
            if (incomeId < 0)
                throw new ValidationException($"incorrect incomeId {incomeId}");

            return await _incomeRepository.RemoveIncome(incomeId);
        }
    }
}
