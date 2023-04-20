namespace SimpleBlog.Models
{
    public class UserDto
    {
        public UserDto()
        {
            Posts = new List<PostDto>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<PostDto> Posts { get; set; }
        public string PasswordHash { get; set; }
    }
}