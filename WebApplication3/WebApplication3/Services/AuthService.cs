using Microsoft.AspNetCore.Identity;
using SimpleBlog.Models;
using SimpleBlog.Repositories;
using System.Security.Cryptography;

namespace SimpleBlog.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }
        public async Task<string> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user == null)
            {
                Console.WriteLine("User not found.");
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                Console.WriteLine("Incorrect password.");
                return null;
            }

            var token = _tokenService.GenerateToken(user);
            Console.WriteLine($"Generated token: {token}");
            return token;
        }

        public async Task<bool> RegisterAsync(User user, string password)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
            if (existingUser != null)
            {
                Console.WriteLine("User already exists.");
                return false;
            }
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            await _userRepository.CreateUserAsync(user);
            return true;
        }

    }
}