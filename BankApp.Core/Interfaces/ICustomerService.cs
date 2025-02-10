using BankApp.Domain.Models;

namespace BankApp.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<int> OpenSavingsAccountAsync(int userId);
        Task<IEnumerable<Account>> GetAccountOverviewAsync(int userId);
        Task<IEnumerable<Account>> GetCustomerAccountsAsync(int customerId);
        Task<(Account Account, IEnumerable<Transaction> Transactions)> GetAccountDetailsAsync(int userId, int accountId);
    }
}
