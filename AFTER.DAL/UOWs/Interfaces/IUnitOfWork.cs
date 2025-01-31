using AFTER.DAL.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace AFTER.DAL.UOWs.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable, IDisposable
    {
        Task<int> CompleteAsync();
        public IUserRepository GetUserRepository();
        public IAuditLogRepository GetAuditLogRepository();
        public ITransmissionStationRepository GetTransmissionStationRepository();
        
    }
}
