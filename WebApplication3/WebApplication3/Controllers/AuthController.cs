using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Models;
using SimpleBlog.Services;
using SimpleBlog.Repositories;

namespace SimpleBlog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly IUserRepository _userRepository;

        public AuthController(IAuthService authService, ITokenService tokenService, IConfiguration configuration,
            IUserRepository userRepository)
        {
            _authService = authService;
            _tokenService = tokenService;
            _configuration = configuration;
            _passwordHasher = new PasswordHasher<User>();
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = new User
            {
                Email = registerDto.Email,
                Username = registerDto.Username,
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, registerDto.Password);

            var result = await _authService.RegisterAsync(user, registerDto.Password);

            if (result)
            {
                return Ok(new
                {
                    user.Id,
                    user.Username,
                    user.Email,
                    Token = _tokenService.GenerateToken(user)
                });
            }

            return BadRequest("An error occurred while registering the user.");
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponse>> Login(LoginDto loginDto)
        {
            var token = await _authService.AuthenticateAsync(loginDto.Email, loginDto.Password);

            if (token == null)
            {
                return Unauthorized();
            }

            var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);

            return Ok(new UserLoginResponse
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Token = token
            });
        }
    }
}