using SimpleBlog.Models;

namespace SimpleBlog.Services
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(string username, string password);
        Task<bool> RegisterAsync(User user, string password);
    }

}