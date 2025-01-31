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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public AFTERContext AFTERContext
        {
            get { return _dbContext as AFTERContext; }
        }

        public UserRepository(AFTERContext context) : base(context)
        {
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _dbContext.Set<User>()
                .FirstOrDefaultAsync(x => x.IsDeleted == false && x.Email == email);
        }

        public async Task<PaginationDataOut<User>> GetAll(UserPageInfo dataIn)
        {
            var query = GetPagingQuery(dataIn);
            return new PaginationDataOut<User>()
            {
                Count = query.Count(),
                Data = await query
                    .OrderByDescending(x => x.Id)
                    .Skip((dataIn.PageInfo.Page - 1) * dataIn.PageInfo.PageSize)
                    .Take(dataIn.PageInfo.PageSize)
                    .AsNoTracking()
                    .ToListAsync()
            };
        }
        private IQueryable<User> GetPagingQuery(UserPageInfo dataIn)
        {
            var query = _dbContext.Set<User>()
                .Where(x => x.IsDeleted == false);

            if (!string.IsNullOrWhiteSpace(dataIn.FilterParams.SearchParam))
                query = query.Where(x => x.FirstName.ToUpper().Contains(dataIn.FilterParams.SearchParam.ToUpper())
                || x.LastName.ToUpper().Contains(dataIn.FilterParams.SearchParam.ToUpper())
                || x.Email.ToUpper().Contains(dataIn.FilterParams.SearchParam.ToUpper()));

            return query;
        }
    }
}
