using BankApp.Core.Interfaces;
using BankApp.Data.DTO;
using BankApp.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILoanService _loanService;

        public TransactionController(ITransactionRepository transactionRepository, ILoanService loanService)
        {
            _transactionRepository = transactionRepository;
            _loanService = loanService;
        }

        [HttpPost("create-loan")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateLoan(CreateLoanDto loanDto)
        {
            await _loanService.CreateLoanAsync(loanDto);
            return Ok(new { Message = "Loan created successfully, and account balance updated." });
        }

        [HttpPost("transfer")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> TransferFunds(TransferDto transferDto)
        {
            await _transactionRepository.TransferFundsAsync(transferDto);
            return Ok();
        }
    }
}
