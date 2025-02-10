using BankApp.Data.DTO;
using BankApp.Data.Interfaces;
using BankApp.Domain.Models;
using Dapper;
using System.Data;

namespace BankApp.Data.Repos
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IDbConnection _dbConnection;

        public TransactionRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> CreateTransactionAsync(Transaction transaction)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AccountID", transaction.AccountID);
            parameters.Add("@Date", transaction.Date);
            parameters.Add("@Type", transaction.Type);
            parameters.Add("@Operation", transaction.Operation ?? "Loan"); // Default value
            parameters.Add("@Amount", transaction.Amount);
            parameters.Add("@Balance", transaction.Balance);
            parameters.Add("@TransactionID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _dbConnection.ExecuteAsync("CreateTransaction", parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<int>("@TransactionID");
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId)
        {
            const string procedure = "GetTransactionsByAccountId";
            return await _dbConnection.QueryAsync<Transaction>(procedure, new { AccountID = accountId }, commandType: CommandType.StoredProcedure);
        }

        public async Task TransferFundsAsync(TransferDto transferDto)
        {
            const string procedure = "TransferFunds";
            var parameters = new
            {
                FromAccountID = transferDto.FromAccountId,
                ToAccountID = transferDto.ToAccountId,
                Amount = transferDto.Amount
            };

            await _dbConnection.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
