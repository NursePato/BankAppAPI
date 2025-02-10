using BankApp.Data.DTO;

namespace BankApp.Core.Interfaces
{
    public interface ILoanService
    {
        Task CreateLoanAsync(CreateLoanDto loanDto);
    }
}
