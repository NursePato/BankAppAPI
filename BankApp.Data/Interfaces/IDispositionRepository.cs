using BankApp.Domain.Models;

namespace BankApp.Data.Interfaces
{
    public interface IDispositionRepository
    {
        Task<int> CreateDispositionAsync(Disposition disposition);
    }

}
