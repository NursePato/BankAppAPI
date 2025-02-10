using BankApp.Domain.Models;

namespace BankApp.Data.Interfaces
{
    public interface IAccountRepository
    {
        public Task<int> CreateAccountAsync(Account account);
        public Task CreateDispositionAsync(Disposition disposition);
        public Task<IEnumerable<Account>> GetAccountsByUserIdAsync(int customerId);
        Task<Disposition> GetDispositionByCustomerAndAccountIdAsync(int customerId, int accountId);
        Task<Account> GetAccountByIdAsync(int accountId);
        Task<IEnumerable<Account>> GetAccountsByCustomerIdAsync(int customerId);

    }
}
