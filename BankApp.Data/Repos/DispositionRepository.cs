using BankApp.Data.Interfaces;
using BankApp.Domain.Models;
using Dapper;
using System.Data;

namespace BankApp.Data.Repos
{
    public class DispositionRepository : IDispositionRepository
    {
        private readonly IDbConnection _dbConnection;

        public DispositionRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<int> CreateDispositionAsync(Disposition disposition)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CustomerID", disposition.CustomerID);
            parameters.Add("@AccountID", disposition.AccountID);
            parameters.Add("@Type", disposition.Type);
            parameters.Add("@DispositionID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _dbConnection.ExecuteAsync("CreateDispositionWithOutputID", parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<int>("@DispositionID");
        }        
    }

}
