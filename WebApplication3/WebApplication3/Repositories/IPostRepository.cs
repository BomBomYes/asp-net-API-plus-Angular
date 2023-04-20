using SimpleBlog.Models;

namespace SimpleBlog.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetPostsAsync();
        Task<Post> GetPostAsync(int id);
        Task<Post> CreatePostAsync(Post post);
        Task UpdatePostAsync(Post post);
        Task DeletePostAsync(int id);
    }

}
