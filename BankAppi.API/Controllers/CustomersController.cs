using BankApp.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;


        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("open-savings")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> OpenSavingsAccount()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new { Message = "User is not logged in or token is invalid." });
            }

            if (!int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new { Message = "Invalid user ID in token" });
            }

            try
            {
                var accountId = await _customerService.OpenSavingsAccountAsync(userId);
                return Ok(new { Message = "Savings account created successfully", AccountId = accountId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpGet("overview")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetAccountOverview()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new { Message = "Invalid or missing user ID in token." });
            }

            try
            {
                var accounts = await _customerService.GetAccountOverviewAsync(userId);
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpGet("accounts/{accountId}/details")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetAccountDetails(int accountId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new { Message = "User is not logged in or token is invalid" });
            }

            try
            {
                var (account, transactions) = await _customerService.GetAccountDetailsAsync(userId, accountId);
                return Ok(new { Account = account, Transactions = transactions });
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}
