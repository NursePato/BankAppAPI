using BankApp.Domain.Models;

namespace BankApp.Data.Interfaces
{
    public interface ICustomerRepository
    {
        public Task<int> CreateCustomerAsync(Customer customer);
        Task<IEnumerable<Account>> GetAccountsByCustomerIdAsync(int customerId);
        Task<Customer> GetCustomerByUserIdAsync(int userId);
    }
}
