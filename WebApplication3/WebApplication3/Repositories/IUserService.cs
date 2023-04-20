using SimpleBlog.Models;

namespace SimpleBlog.Repositories
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserAsync(int id);
        Task<User> CreateUserAsync(UserDto userDto);
        Task UpdateUserAsync(int id, User user);
        Task DeleteUserAsync(int id);
    }
}
