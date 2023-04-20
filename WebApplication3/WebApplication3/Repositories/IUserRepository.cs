using SimpleBlog.Models;

namespace SimpleBlog.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserAsync(int id);
        Task<User> CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> AddUserAsync(User user);
    }
}
