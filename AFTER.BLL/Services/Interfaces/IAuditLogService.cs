using System.Threading.Tasks;

namespace AFTER.BLL.Services.Interfaces
{
    public interface IAuditLogService
    {
        Task Save(string page, string actionName, object request);
    }
}
