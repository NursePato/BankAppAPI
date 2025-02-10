using BankApp.Domain.Models;
namespace BankApp.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        public Task<int> CreateUserAsync(User user);
    }
}
