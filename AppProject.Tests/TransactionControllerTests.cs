using BankApi.API.Controllers;
using BankApp.Core.Interfaces;
using BankApp.Data.DTO;
using BankApp.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

public class TransactionControllerTests
{
    [Fact]
    public async Task TransferFunds_ShouldReturnOkResult_WhenTransferIsSuccessful()
    {
        //Arrange
        var mockTransactionRepo = new Mock<ITransactionRepository>();    
        var mockLoanService = new Mock<ILoanService>();

        mockTransactionRepo
            .Setup(repo => repo.TransferFundsAsync(It.IsAny<TransferDto>()))
            .Returns(Task.CompletedTask);

        var controller = new TransactionController(mockTransactionRepo.Object, mockLoanService.Object);

        var transferDto = new TransferDto
        {
            FromAccountId = 1,
            ToAccountId = 2,
            Amount = 5000
        };

        //Act
        var result = await controller.TransferFunds(transferDto);

        //Assert
        Assert.IsType<OkResult>(result);

        //Verify that TransferFundsAsync was called exactly once
        mockTransactionRepo.Verify(repo => repo.TransferFundsAsync(It.Is<TransferDto>(t =>
            t.FromAccountId == transferDto.FromAccountId &&
            t.ToAccountId == transferDto.ToAccountId &&
            t.Amount == transferDto.Amount)), Times.Once);
    }
}
