using BankApp.Data.Interfaces;
using BankApp.Domain.Models;
using Dapper;
using System.Data;

namespace BankApp.Data.Repos
{
    public class LoanRepository : ILoanRepository
    {
        private readonly IDbConnection _dbConnection;

        public LoanRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<int> CreateLoanAsync(Loan loan)
        {
            var parameters = new
            {
                loan.AccountID,
                loan.Date,
                loan.Amount,
                loan.Duration,
                loan.Payments,
                loan.Status
            };

            const string procedure = "CreateLoan";
            return await _dbConnection.ExecuteScalarAsync<int>(procedure, parameters, commandType: CommandType.StoredProcedure);
        }
    }

}
