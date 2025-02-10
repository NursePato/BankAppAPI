using BankApp.Core.Interfaces;
using BankApp.Core.Services;
using BankApp.Data.DataModels;
using BankApp.Data.Interfaces;
using BankApp.Data.Repos;
namespace BankApi.API.Services
{
    public static class ServiceExtrension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IBankAppDataContext, BankAppDataContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IDispositionRepository, DispositionRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ILoanRepository, LoanRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ILoanService, LoanService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<LoanService>();
            services.AddScoped<TokenService>();
        }
    }
}
