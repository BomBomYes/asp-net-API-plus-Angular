using Microsoft.EntityFrameworkCore;
using SimpleBlog.Models;

namespace SimpleBlog.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PostRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            return await _dbContext.Posts.ToListAsync();
        }

        public async Task<Post> GetPostAsync(int id)
        {
            return await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Post> CreatePostAsync(Post post)
        {
            _dbContext.Posts.Add(post);
            await _dbContext.SaveChangesAsync();
            return post;
        }

        public async Task UpdatePostAsync(Post post)
        {
            _dbContext.Entry(post).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int id)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (post != null)
            {
                _dbContext.Posts.Remove(post);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}