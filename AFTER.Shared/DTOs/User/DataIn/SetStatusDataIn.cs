using AFTER.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTER.Shared.DTOs.User.DataIn
{
    public class SetStatusDataIn
    {
        public int Id { get; set; }
        public UserStatus? Status { get; set; }
    }
}
