using AFTER.DAL.Model;
using AFTER.Shared.DTOs.Pagination;
using AFTER.Shared.DTOs.User.DataIn;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AFTER.DAL.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmail(string email);
        Task<PaginationDataOut<User>> GetAll(UserPageInfo dataIn);

    }
}
