using Microsoft.EntityFrameworkCore;
using SimpleBlog.Models;

namespace SimpleBlog.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
        }


        public async Task<bool> AddUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}