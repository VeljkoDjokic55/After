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
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Dt> Dts { get; set; }
        public virtual DbSet<Feeder11> Feeder11s { get; set; }
        public virtual DbSet<Feeder33> Feeder33s { get; set; }
        public virtual DbSet<Feeder33Ss> Feeder33Sss { get; set; }
        public virtual DbSet<Substation> Substations { get; set; }
        public virtual DbSet<TransmissionStation> TransmissionStations { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<Pole> Poles { get; set; }
        public virtual DbSet<Meter> Meters { get; set; }
        public virtual DbSet<Slrn> Slrns { get; set; }

        public virtual DbSet<AuditLog> AuditLogs { get; set; }
    }
}
