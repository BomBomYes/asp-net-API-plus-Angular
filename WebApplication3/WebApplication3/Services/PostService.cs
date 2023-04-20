using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Models;
using SimpleBlog.Repositories;

namespace SimpleBlog.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _context;

        public PostService(IPostRepository postRepository, IMapper mapper, IUserRepository userRepository,
            ApplicationDbContext context)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _context = context;
        }

        // Добавьте параметр search


        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            return await _context.Posts.ToListAsync();
        }

        public Task<List<Post>> FindPostByTitle(string search)
        {
            return _context.Posts.Where(p => p.Title.Contains(search)).ToListAsync();
        }

        public async Task<Post> GetPostAsync(int id)
        {
            return await _postRepository.GetPostAsync(id);
        }

        public async Task<Post> CreatePostAsync(PostDto postDto)
        {
            User user = await _userRepository.GetUserAsync(postDto.UserId);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            Post newPost = _mapper.Map<Post>(postDto);
            newPost.User = user;
            return await _postRepository.CreatePostAsync(newPost);
        }

        public async Task UpdatePostAsync(int id, Post post)
        {
            if (id != post.Id)
            {
                throw new ArgumentException("Post ID does not match.");
            }

            await _postRepository.UpdatePostAsync(post);
        }

        public async Task DeletePostAsync(int id)
        {
            await _postRepository.DeletePostAsync(id);
        }
    }
}