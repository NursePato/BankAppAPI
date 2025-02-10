using Microsoft.Data.SqlClient;

namespace BankApp.Data.Interfaces
{
    public interface IBankAppDataContext
    {
        public SqlConnection GetConnection();
    }
}
