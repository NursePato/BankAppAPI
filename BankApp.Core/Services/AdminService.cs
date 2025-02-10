using AutoMapper;
using BankApp.Core.Interfaces;
using BankApp.Data.DTO;
using BankApp.Data.Interfaces;
using BankApp.Domain.Models;

namespace BankApp.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AdminService(IUserRepository userRepository, 
            ICustomerRepository customerRepository, 
            IAccountRepository accountRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }
        public async Task<(int UserId, int AccountId)> CreateCustomerAsync(CreateCustomerDto customerDto)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(customerDto.Password);

            var user = new User
            {
                Username = customerDto.Username,
                PasswordHash = passwordHash,
                Role = "Customer",
                CreatedAt = DateTime.UtcNow
            };
            var userId = await _userRepository.CreateUserAsync(user);

            //Automap the DTO to Customer & assign UserId
            var customer = _mapper.Map<Customer>(customerDto);
            customer.UserId = userId;
            var customerId = await _customerRepository.CreateCustomerAsync(customer);

            //AutoMap DTO to Account
            var account = _mapper.Map<Account>(customerDto);
            account.Created = DateTime.UtcNow;

            //1 = Standard transaction account
            account.AccountTypesID = 1;
            var accountId = await _accountRepository.CreateAccountAsync(account);

            //Disposition linking customer and account
            var disposition = new Disposition
            {
                CustomerID = customerId,
                AccountID = accountId,
                Type = "Owner"
            };
            await _accountRepository.CreateDispositionAsync(disposition);

            return (userId, accountId);
        }
    }
}
