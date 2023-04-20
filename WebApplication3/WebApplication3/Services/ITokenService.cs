using SimpleBlog.Models;

namespace SimpleBlog.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }

}
