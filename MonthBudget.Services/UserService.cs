using MonthBudget.Data.Models;
using MonthBudget.Data.Repositories;
using MonthBudget.ServiceContracts;
using System.ComponentModel.DataAnnotations;

namespace MonthBudget.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public User GetUser(int userId)
        {
            if (userId < 0)
                throw new ValidationException($"incorrect userid {userId}");

            var user = _userRepository.GetById(userId);
            return user ?? throw new NullReferenceException($"can not find a user with id {userId}");
        }

        public User GetUserWithAllTransactions(int userId)
        {
            if (userId < 0)
                throw new ValidationException($"incorrect userid {userId}");

            var user = _userRepository.GetByIdWithAllTransactions(userId);
            return user ?? throw new NullReferenceException($"can not find a user with id {userId}");
        }

        public User GetUserWithMonthTransactions(int userId, int month, int year)
        {
            if (userId < 0)
                throw new ValidationException($"incorrect userid {userId}");

            if (month < 1 || month > 12)
                throw new ValidationException($"incorrect month {month}");

            if (year < 2020 || year > 2030)
                throw new ValidationException($"incorrect year {year}");

            var from = new DateTime(year, month, 1);
            var to = from.AddMonths(1);

            var user = _userRepository.GetByIdWithRangeTransactions(userId, from, to);
            return user ?? throw new NullReferenceException($"can not find a user with id {userId}");
        }
    }
}