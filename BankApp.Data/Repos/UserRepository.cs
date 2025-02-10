using BankApp.Data.Interfaces;
using BankApp.Domain.Models;
using Dapper;
using System.Data;

namespace BankApp.Data.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;

        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> CreateUserAsync(User user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Username", user.Username);
            parameters.Add("@PasswordHash", user.PasswordHash);
            parameters.Add("@Role", user.Role);
            parameters.Add("@CreatedAt", user.CreatedAt);
            parameters.Add("@UserId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _dbConnection.ExecuteAsync("CreateUser", parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<int>("@UserId");
        }
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<User>(
                "GetUserByUsername",
                new { Username = username },
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
