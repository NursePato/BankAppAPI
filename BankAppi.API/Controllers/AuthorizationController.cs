using AutoMapper;
using BankApp.Data.DataModels;
using BankApp.Data.DTO;
using BankApp.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;


        public AuthorizationController(IUserRepository userRepository, TokenService tokenService, IMapper mapper)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(loginDto.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                return Unauthorized(new { Message = "Invalid username or password" });
            }

            var token = _tokenService.GenerateJwtToken(user);

            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = token;

            return Ok(userDto);
        }
    }
}
