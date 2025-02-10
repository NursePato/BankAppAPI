using BankApp.Domain.Models;

namespace BankApp.Data.Interfaces
{
    public interface ILoanRepository
    {
        Task<int> CreateLoanAsync(Loan loan);
    }

}
