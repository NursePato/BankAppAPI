using BankApp.Data.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BankApp.Data.DataModels
{
    public class BankAppDataContext : IBankAppDataContext
    {
        private readonly string _context;
            public BankAppDataContext(IConfiguration config)
        {
            _context = config.GetConnectionString("BankAppData");
        }
        public SqlConnection GetConnection()
        {
            return new SqlConnection(_context);
        }
    }
}
