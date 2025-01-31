using AFTER.Shared.Common;
using AFTER.Shared.Constants;
using AFTER.Shared.DTOs.Pagination;
using AFTER.Shared.DTOs.User.DataIn;
using AFTER.Shared.DTOs.User.DataOut;
using System.Threading.Tasks;

namespace AFTER.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponsePackage<UserDto>> Get(string email, string password);
        Task<ResponsePackage<UserDto>> GetByEmail(string email);
        Task<ResponsePackage<string>> GetResetCodeForEmail(string email);
        Task<ResponsePackage<string>> ResetPassword(string email, string password, string code);
        Task<ResponsePackage<string>> ResetPasswordByRole(string email, string password, string code);
        Task<ResponsePackage<string>> ForgotPassword(string email);
        Task<ResponsePackage<string>> Save(UserDataIn dataIn);
        Task<ResponsePackage<PaginationDataOut<UserDto>>> GetAll(UserPageInfo dataIn);
        Task<ResponsePackage<bool>> SetStatus(SetStatusDataIn dataIn);




    }
}
