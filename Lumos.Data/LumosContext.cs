using Lumos.Data.Models.Management;
using Microsoft.EntityFrameworkCore;

namespace Lumos.Data
{
    public class LumosContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Address> Address { get; set; }

        public LumosContext(DbContextOptions<LumosContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Tenant)
                .WithMany(t => t.Users)
                .HasForeignKey(u => u.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Organization)
                .WithMany(o => o.Users)
                .HasForeignKey(u => u.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Organization>()
                .HasOne(o => o.Tenant)
                .WithMany(t => t.Organizations)
                .HasForeignKey(o => o.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tenant>()
                .HasMany(t => t.Users)
                .WithOne(u => u.Tenant)
                .HasForeignKey(u => u.TenantId);

            modelBuilder.Entity<Tenant>()
                .HasMany(t => t.Organizations)
                .WithOne(o => o.Tenant)
                .HasForeignKey(o => o.TenantId);


        }
    }
}
