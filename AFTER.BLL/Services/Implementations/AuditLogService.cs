using AFTER.BLL.Services.Interfaces;
using AFTER.DAL.Model;
using AFTER.DAL.UOWs.Interfaces;
using System;
using System.Threading.Tasks;

namespace AFTER.BLL.Services.Implementations
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IUnitOfWork _uow;
        private readonly int? _userId;

        public AuditLogService(IUnitOfWork unitOfWork, IHttpContextService httpContext)
        {
            _uow = unitOfWork;
            _userId = httpContext.GetUserId();
        }
        public async Task Save(string page, string actionName, object request)
        {
            await _uow.GetAuditLogRepository().AddAsync(new AuditLog()
            {
                Page = page,
                Action = actionName,
                Time = DateTime.Now,
                UserId = _userId
            });

            await _uow.CompleteAsync();
        }
    }
}
