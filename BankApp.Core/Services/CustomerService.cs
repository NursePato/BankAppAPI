using BankApp.Core.Interfaces;
using BankApp.Data.Interfaces;
using BankApp.Domain.Models;

namespace BankApp.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public CustomerService(
            ICustomerRepository customerRepository,
            IAccountRepository accountRepository,
            ITransactionRepository transactionRepository)
        {
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }
        public async Task<int> OpenSavingsAccountAsync(int userId)
        {
            //Does user exist?
            var customer = await _customerRepository.GetCustomerByUserIdAsync(userId);
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }

            //Creating new account
            var accountId = await _accountRepository.CreateAccountAsync(new Account
            {
                Frequency = "Monthly",
                Created = DateTime.UtcNow,
                Balance = 0,
                AccountTypesID = 2
            });

            //Linking account to customer
            await _accountRepository.CreateDispositionAsync(new Disposition
            {
                CustomerID = customer.CustomerId,
                AccountID = accountId,
                Type = "Owner"
            });

            return accountId;
        }

        public async Task<IEnumerable<Account>> GetAccountOverviewAsync(int userId)
        {
            //GetCustomerByUser
            var customer = await _customerRepository.GetCustomerByUserIdAsync(userId);
            if (customer == null)
            {
                throw new Exception("Customer not found.");
            }

            //GetAccountsByCustomerId
            var accounts = await _accountRepository.GetAccountsByCustomerIdAsync(customer.CustomerId);
            if (accounts == null || !accounts.Any())
            {
                throw new Exception("No accounts found for this customer.");
            }

            return accounts;
        }
        public async Task<IEnumerable<Account>> GetCustomerAccountsAsync(int customerId)
        {
            return await _accountRepository.GetAccountsByUserIdAsync(customerId);
        }

        public async Task<(Account Account, IEnumerable<Transaction> Transactions)> GetAccountDetailsAsync(int userId, int accountId)
        {
            //Does user exist?
            var customer = await _customerRepository.GetCustomerByUserIdAsync(userId);
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }

            //Verify that the account belongs to the customer
            var disposition = await _accountRepository.GetDispositionByCustomerAndAccountIdAsync(customer.CustomerId, accountId);
            if (disposition == null)
            {
                throw new UnauthorizedAccessException("You do not have access to this account");
            }

            //Get account and transactions
            var account = await _accountRepository.GetAccountByIdAsync(accountId);
            if (account == null)
            {
                throw new Exception("Account not found");
            }

            var transactions = await _transactionRepository.GetTransactionsByAccountIdAsync(accountId);
            return (account, transactions);
        }
    }
}
