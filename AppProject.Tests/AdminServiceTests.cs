using AutoMapper;
using BankApp.Core.Services;
using BankApp.Data.DTO;
using BankApp.Data.Interfaces;
using BankApp.Data.Profiles;
using BankApp.Domain.Models;
using Moq;

public class AdminServiceTests
{
    [Fact]
    public async Task CreateCustomerAsync_ShouldCreateCustomerAndAssignAccount()
    {
        //Arrange
        var mockUserRepo = new Mock<IUserRepository>();
        var mockCustomerRepo = new Mock<ICustomerRepository>();
        var mockAccountRepo = new Mock<IAccountRepository>();

        mockUserRepo
            .Setup(repo => repo.CreateUserAsync(It.IsAny<User>()))
            .ReturnsAsync(1);

        mockCustomerRepo
            .Setup(repo => repo.CreateCustomerAsync(It.IsAny<Customer>()))
            .ReturnsAsync(1);

        mockAccountRepo
            .Setup(repo => repo.CreateAccountAsync(It.IsAny<Account>()))
            .ReturnsAsync(1001);

        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        var mapper = config.CreateMapper();

        var adminService = new AdminService(mockUserRepo.Object, mockCustomerRepo.Object, mockAccountRepo.Object, mapper);

        var createCustomerDto = new CreateCustomerDto
        {
            Gender = "Male",
            Username = "KalleKarl",
            Password = "password123",
            GivenName = "Karl",
            Surname = "Karlsson",
            StreetAddress = "Kallegatan",
            City = "Karlstad",
            ZipCode = "12345",
            Country = "Karlland",
            CountryCode = "KL",
            Birthday = new DateTime(1990, 1, 1),
            TelephoneNumber = "123456789",
            EmailAddress = "karl@gmail.com",
            AccountTypeId = 1,
            InitialDeposit = 1000.00m
        };

        //Act
        var result = await adminService.CreateCustomerAsync(createCustomerDto);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.UserId);
        Assert.Equal(1001, result.AccountId);

        //Verify repository interactions
        mockUserRepo.Verify(repo => repo.CreateUserAsync(It.IsAny<User>()), Times.Once);
        mockCustomerRepo.Verify(repo => repo.CreateCustomerAsync(It.IsAny<Customer>()), Times.Once);
        mockAccountRepo.Verify(repo => repo.CreateAccountAsync(It.IsAny<Account>()), Times.Once);
    }
}
