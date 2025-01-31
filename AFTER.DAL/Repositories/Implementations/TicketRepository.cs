using AFTER.DAL.Context;
using AFTER.DAL.Model;
using AFTER.DAL.Repositories.Interfaces;
using AFTER.Shared.DTOs.Pagination;
using AFTER.Shared.DTOs.User.DataIn;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTER.DAL.Repositories.Implementations
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public AFTERContext AFTERContext
        {
            get { return _dbContext as AFTERContext; }
        }

        public TicketRepository(AFTERContext context) : base(context)
        {
        }

    }
}
