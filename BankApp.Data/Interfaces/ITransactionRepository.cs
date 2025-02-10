using BankApp.Data.DTO;
using BankApp.Domain.Models;

namespace BankApp.Data.Interfaces
{
    public interface ITransactionRepository
    {
        Task<int> CreateTransactionAsync(Transaction transaction);
        Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId);
        Task TransferFundsAsync(TransferDto transferDto);
    }

}
