using AFTER.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace AFTER.DAL.Context
{
    public class AFTERContext : DbContext
    {
        public AFTERContext(DbContextOptions<AFTERContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        public virtual DbSet<Ticket> Tickets{ get; set; }
    }
}
