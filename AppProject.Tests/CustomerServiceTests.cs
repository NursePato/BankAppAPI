using BankApp.Core.Services;
using BankApp.Data.Interfaces;
using BankApp.Domain.Models;
using Moq;

public class CustomerServiceTests
{
    [Fact]
    public async Task GetAccountDetails_ShouldReturnAccountAndTransactions()
    {
        //Arrange
        var mockCustomerRepo = new Mock<ICustomerRepository>();
        var mockAccountRepo = new Mock<IAccountRepository>();
        var mockTransactionRepo = new Mock<ITransactionRepository>();

        var userId = 12345;
        var accountId = 67890;

        mockCustomerRepo
            .Setup(repo => repo.GetCustomerByUserIdAsync(userId))
            .ReturnsAsync(new Customer { CustomerId = 1, UserId = userId, Gender = "Male" });

        mockAccountRepo
            .Setup(repo => repo.GetDispositionByCustomerAndAccountIdAsync(It.IsAny<int>(), accountId))
            .ReturnsAsync(new Disposition { CustomerID = 1, AccountID = accountId }); 

        var account = new Account { AccountID = accountId, UserId = userId, Balance = 5000 };
        mockAccountRepo
            .Setup(repo => repo.GetAccountByIdAsync(accountId))
            .ReturnsAsync(account);

        var transactions = new List<Transaction>
    {
        new Transaction { TransactionId = 1, Amount = 1000 },
        new Transaction { TransactionId = 2, Amount = 2000 }
    };
        mockTransactionRepo
            .Setup(repo => repo.GetTransactionsByAccountIdAsync(accountId))
            .ReturnsAsync(transactions);

        var customerService = new CustomerService(mockCustomerRepo.Object, mockAccountRepo.Object, mockTransactionRepo.Object);

        //Act
        var result = await customerService.GetAccountDetailsAsync(userId, accountId);

        //Assert
        Assert.Equal(account, result.Account);
        Assert.Equal(transactions, result.Transactions);

        //Verify repository methods were called
        mockCustomerRepo.Verify(repo => repo.GetCustomerByUserIdAsync(userId), Times.Once);
        mockAccountRepo.Verify(repo => repo.GetAccountByIdAsync(accountId), Times.Once);
        mockTransactionRepo.Verify(repo => repo.GetTransactionsByAccountIdAsync(accountId), Times.Once);
    }
}
