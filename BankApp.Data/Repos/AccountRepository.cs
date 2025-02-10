using BankApp.Data.Interfaces;
using BankApp.Domain.Models;
using Dapper;
using System.Data;

namespace BankApp.Data.Repos
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IBankAppDataContext _dbDataContext;

        public AccountRepository(IDbConnection connection, IBankAppDataContext dbDataContext)
        {
            _dbConnection = connection;
            _dbDataContext = dbDataContext;
        }
        public async Task<int> CreateAccountAsync(Account account)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Frequency", account.Frequency);
            parameters.Add("@Created", account.Created);
            parameters.Add("@Balance", account.Balance);
            parameters.Add("@AccountTypesID", account.AccountTypesID);
            parameters.Add("@AccountID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _dbConnection.ExecuteAsync("CreateAccount", parameters, commandType: CommandType.StoredProcedure);
            return parameters.Get<int>("@AccountID");
        }

        public async Task CreateDispositionAsync(Disposition disposition)
        {
            var parameters = new
            {
                disposition.CustomerID,
                disposition.AccountID,
                disposition.Type
            };

            await _dbConnection.ExecuteAsync("CreateDisposition", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Account>> GetAccountsByUserIdAsync(int customerId)
        {
            return await _dbConnection.QueryAsync<Account>(
                "GetAccountsByUserId",
                new { CustomerId = customerId },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<Disposition> GetDispositionByCustomerAndAccountIdAsync(int customerId, int accountId)
        {
            return await _dbConnection.QuerySingleOrDefaultAsync<Disposition>(
                "GetDispositionByCustomerAndAccountId",
                new { CustomerId = customerId, AccountId = accountId },
                commandType: CommandType.StoredProcedure);
        }
        public async Task<Account> GetAccountByIdAsync(int accountId)
        {
            using (var connection = _dbDataContext.GetConnection())
            {
                var account = await connection.QuerySingleOrDefaultAsync<Account>(
                    "GetAccountById",
                    new { AccountId = accountId },
                    commandType: CommandType.StoredProcedure);
                return account;
            }
        }

        public async Task<IEnumerable<Account>> GetAccountsByCustomerIdAsync(int customerId)
        {
            using (var connection = _dbDataContext.GetConnection())
            {
                var accounts = await connection.QueryAsync<Account>(
                    "GetAccountsByCustomerId",
                    new { CustomerId = customerId },
                    commandType: CommandType.StoredProcedure);

                return accounts;
            }
        }
    }
}
