using AFTER.Shared.Common;
using System.Threading.Tasks;

namespace AFTER.BLL.Services.Interfaces
{
    public interface IEmailService
    {
        Task<ResponsePackageNoData> SendMailAsync(EmailData emailData);
    }
}
