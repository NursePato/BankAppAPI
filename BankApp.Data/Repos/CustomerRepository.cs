using BankApp.Data.Interfaces;
using BankApp.Domain.Models;
using Dapper;
using System.Data;

namespace BankApp.Data.Repos
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbConnection _dbConnection;

        public CustomerRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<int> CreateCustomerAsync(Customer customer)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", customer.UserId);
            parameters.Add("@Gender", customer.Gender);
            parameters.Add("@Givenname", customer.Givenname);
            parameters.Add("@Surname", customer.Surname);
            parameters.Add("@Streetaddress", customer.Streetaddress);
            parameters.Add("@City", customer.City);
            parameters.Add("@Zipcode", customer.Zipcode);
            parameters.Add("@Country", customer.Country);
            parameters.Add("@CountryCode", customer.CountryCode);
            parameters.Add("@Birthday", customer.Birthday);
            parameters.Add("@Telephonenumber", customer.Telephonenumber);
            parameters.Add("@Emailaddress", customer.Emailaddress);
            parameters.Add("@CustomerId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _dbConnection.ExecuteAsync("CreateCustomer", parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<int>("@CustomerId");
        }
        public async Task<IEnumerable<Account>> GetAccountsByCustomerIdAsync(int customerId)
        {
                return await _dbConnection.QueryAsync<Account>(
                    "GetAccountsByCustomerId",
                    new { CustomerId = customerId },
                    commandType: CommandType.StoredProcedure);
        }
        public async Task<Customer> GetCustomerByUserIdAsync(int userId)
        {
            return await _dbConnection.QuerySingleOrDefaultAsync<Customer>(
                "GetCustomerByUserId",
                new { UserId = userId },
                commandType: CommandType.StoredProcedure);
        }
    }
}
