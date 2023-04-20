using SimpleBlog.Models;

namespace SimpleBlog.Services
{
    public interface IPostService
    {
        Task<Post> GetPostAsync(int id);
        Task<Post> CreatePostAsync(PostDto postDto);
        Task UpdatePostAsync(int id, Post post);
        Task DeletePostAsync(int id);

        Task<IEnumerable<Post>> GetPostsAsync();
        Task<List<Post>> FindPostByTitle(string search);
    }
}