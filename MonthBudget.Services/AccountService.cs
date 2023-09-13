using MonthBudget.Data.Models;
using MonthBudget.Data.Repositories;
using MonthBudget.ServiceContracts;
using System.ComponentModel.DataAnnotations;

namespace MonthBudget.Services
{
    public class AccountService : IAccountService
    {
        private readonly AccountRepository _accountRepository;

        public AccountService(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Account> AddAccount(Account account)
        {
            // Validate the expense object
            var validationContext = new ValidationContext(account, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(account, validationContext, validationResults, validateAllProperties: true);

            if (!isValid)
            {
                // If there are validation errors, collect the error messages and throw a ValidationException
                var errorMessages = validationResults.Select(result => result.ErrorMessage);
                string errorMessage = string.Join("; ", errorMessages);
                throw new ValidationException(errorMessage);
            }
            return await _accountRepository.AddAccount(account);
        }


        public List<Account> GetAccounts(int userId)
        {
            if (userId < 0)
                throw new ValidationException($"incorrect userid {userId}");

            return _accountRepository.GetAll(userId);
        }

        public async Task<bool> RemoveAccount(int accountId)
        {
            if (accountId < 0)
                throw new ValidationException($"incorrect accountId {accountId}");

            return await _accountRepository.RemoveAccount(accountId);
        }
    }
}