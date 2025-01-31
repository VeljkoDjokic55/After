using AFTER.DAL.Context;
using AFTER.DAL.Model;
using AFTER.DAL.Repositories.Interfaces;

namespace AFTER.DAL.Repositories.Implementations
{
    internal class AuditLogRepository : Repository<AuditLog>, IAuditLogRepository
    {
        public AFTERContext AFTERContext
        {
            get { return _dbContext as AFTERContext; }
        }

        public AuditLogRepository(AFTERContext context) : base(context)
        {
        }
    }
}
