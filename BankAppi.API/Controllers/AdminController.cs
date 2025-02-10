using BankApp.Core.Interfaces;
using BankApp.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("create-customer")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto customerDto)
        {
            try
            {
                var (userId, accountId) = await _adminService.CreateCustomerAsync(customerDto);
                return Ok(new { Message = "Customer created successfully", UserId = userId, AccountId = accountId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An error occurred while creating the customer", Details = ex.Message });
            }
        }
    }
}
