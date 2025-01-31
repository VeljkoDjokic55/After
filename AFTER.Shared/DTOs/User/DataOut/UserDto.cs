
using AFTER.Shared.Constants;

namespace AFTER.Shared.DTOs.User.DataOut
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public Role? Role { get; set; }
        public UserStatus? Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
