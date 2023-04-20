using System.ComponentModel.DataAnnotations;

namespace SimpleBlog.Models
{
    // DTO-модель для авторизации пользователя
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}