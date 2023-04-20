namespace SimpleBlog.Models
{
    public class PostDto
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}