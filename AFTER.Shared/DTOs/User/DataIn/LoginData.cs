using System.ComponentModel.DataAnnotations;

namespace AFTER.Shared.DTOs.User.DataIn
{
    public class LoginData
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
