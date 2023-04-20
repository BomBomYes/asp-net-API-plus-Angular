﻿namespace SimpleBlog.Models
{
    // Post.cs
    public class Post
    {
        public Post()
        {
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
