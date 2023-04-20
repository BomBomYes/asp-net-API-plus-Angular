using System.ComponentModel.DataAnnotations;

namespace SimpleBlog.Models
{
    // DTO-модель для регистрации нового пользователя
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        public string Username { get; set; }
    }
}
