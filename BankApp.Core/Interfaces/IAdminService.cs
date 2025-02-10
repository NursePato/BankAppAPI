using BankApp.Data.DTO;

namespace BankApp.Core.Interfaces
{
    public interface IAdminService
    {
        Task<(int UserId, int AccountId)> CreateCustomerAsync(CreateCustomerDto customerDto);
    }
}
