using AFTER.Shared.Constants;
using System;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTER.Shared.DTOs.User.DataIn
{
    public class UserDataIn
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ResetCode { get; set; }
        public Role? Role { get; set; }
        public UserStatus? Status { get; set; }
        public string SearchParam { get; set; }

    }
}
