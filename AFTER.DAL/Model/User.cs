using AFTER.Shared.Constants;

namespace AFTER.DAL.Model
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ResetCode { get; set; }
        public Role? Role { get; set; }
        public UserStatus? Status { get; set; }
    }
}
