using AFTER.DAL.Context;
using AFTER.DAL.Repositories.Implementations;
using AFTER.DAL.Repositories.Interfaces;
using AFTER.DAL.UOWs.Interfaces;
using System;
using System.Threading.Tasks;

namespace AFTER.DAL.UOWs.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private AFTERContext _context;
        private IUserRepository UserRepository { get; set; }
        private IAuditLogRepository AuditLogRepository { get; set; }
        private ITicketRepository TicketRepository { get; set; }

        public UnitOfWork(AFTERContext context)
        {
            _context = context;
        }

        public IUserRepository GetUserRepository()
        {
            return UserRepository ??= new UserRepository(_context);
        }

        public IAuditLogRepository GetAuditLogRepository()
        {
            return AuditLogRepository ??= new AuditLogRepository(_context);
        }

        public ITicketRepository GetTicketRepository()
        {
            return TicketRepository ??= new TicketRepository(_context);
        }


        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore();
            Dispose(disposing: false);
            GC.SuppressFinalize(this);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _context?.Dispose();

            _context = null;
        }

        protected virtual async ValueTask DisposeAsyncCore()
        {
            if (_context != null)
                await _context.DisposeAsync();

            _context = null;
        }
    }
}
