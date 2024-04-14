using Lumos.Data.Models;
using Lumos.Data.Models.Management;
using Microsoft.EntityFrameworkCore;

namespace Lumos.Data
{
    public class LumosContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Units> Units { get; set; }
        public DbSet<Tenants> Tenants { get; set; }

        public LumosContext(DbContextOptions<LumosContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tenants>()
                .HasMany(t => t.Units)
                .WithOne(u => u.Tenant)
                .HasForeignKey(u => u.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Users>()
                .HasOne(u => u.Tenant)
                .WithMany()
                .HasForeignKey(u => u.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            ConfigureEnumerations(modelBuilder);
        }

        private void ConfigureEnumerations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tenants>()
                .Property(u => u.Type)
                .HasConversion<string>();

            modelBuilder.Entity<Tenants>()
                .Property(u => u.State)
                .HasConversion<string>();

            modelBuilder.Entity<Units>()
                .Property(u => u.Level)
                .HasConversion<string>();
        }
    }
}
