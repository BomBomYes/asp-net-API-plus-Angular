using AutoMapper;
using SimpleBlog.Models;
using SimpleBlog.Repositories;

namespace SimpleBlog.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _userRepository.GetUsersAsync();
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _userRepository.GetUserAsync(id);
        }

        public async Task<User> CreateUserAsync(UserDto userDto)
        {
            User newUser = _mapper.Map<User>(userDto);

            foreach (var post in newUser.Posts)
            {
                post.User = newUser;
            }

            return await _userRepository.CreateUserAsync(newUser);
        }

        public async Task UpdateUserAsync(int id, User user)
        {
            if (id != user.Id)
            {
                throw new ArgumentException("User ID does not match.");
            }

            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteUserAsync(id);
        }
    }

}
