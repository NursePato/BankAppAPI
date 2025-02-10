using BankApp.Core.Interfaces;
using BankApp.Data.DTO;
using BankApp.Data.Interfaces;
using Dapper;
using System.Data;

namespace BankApp.Core.Services
{
    public class LoanService : ILoanService
    {
        private readonly IDbConnection _dbConnection;

        public LoanService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task CreateLoanAsync(CreateLoanDto loanDto)
        {
            await _dbConnection.ExecuteAsync(
                "LoanServiceCreateLoan",
                new
                {
                    AccountID = loanDto.AccountID,
                    Amount = loanDto.Amount,
                    Date = DateTime.Now,
                    Duration = loanDto.Duration,
                    Payments = loanDto.Payments,
                    Status = "Active"
                },
                commandType: CommandType.StoredProcedure
            );

            //Current balance
            var parameters = new DynamicParameters();
            parameters.Add("@AccountID", loanDto.AccountID);
            parameters.Add("@Balance", dbType: DbType.Decimal, direction: ParameterDirection.Output);

            await _dbConnection.ExecuteAsync(
                "LoanServiceGetAccountBalance",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            var currentBalance = parameters.Get<decimal>("@Balance");

            //calculate new balance
            var newBalance = currentBalance + loanDto.Amount;

            await _dbConnection.ExecuteAsync(
                "UpdateAccountBalanceAndInsertTransaction",
                new
                {
                    AccountID = loanDto.AccountID,
                    Amount = loanDto.Amount,
                    NewBalance = newBalance,
                    Type = "Credit",
                    Operation = "Loan Disbursement",
                    Date = DateTime.Now
                },
                commandType: CommandType.StoredProcedure
            );
        }


    }
}
