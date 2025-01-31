using AFTER.Shared.DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTER.Shared.DTOs.User.DataIn
{
    public class UserPageInfo
    {
        public PageInfo PageInfo { get; set; }
        public UserDataIn FilterParams { get; set; }
    }
}
