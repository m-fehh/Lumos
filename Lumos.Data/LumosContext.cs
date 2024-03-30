using Lumos.Data.Models.Management;
using Microsoft.EntityFrameworkCore;

namespace Lumos.Data
{
    public class LumosContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Organizations> Organizations { get; set; }
        public DbSet<Tenants> Tenants { get; set; }
        public DbSet<Address> Address { get; set; }

        public LumosContext(DbContextOptions<LumosContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .HasOne(u => u.Tenant)
                .WithMany(t => t.Users)
                .HasForeignKey(u => u.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Users>()
                .HasOne(u => u.Organization)
                .WithMany(o => o.Users)
                .HasForeignKey(u => u.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Users>()
                .HasOne(u => u.Address)
                .WithOne(a => a.User)
                .HasForeignKey<Address>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Organizations>()
                .HasOne(o => o.Tenant)
                .WithMany(t => t.Organizations)
                .HasForeignKey(o => o.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tenants>()
                .HasMany(t => t.Users)
                .WithOne(u => u.Tenant)
                .HasForeignKey(u => u.TenantId);

            modelBuilder.Entity<Tenants>()
                .HasMany(t => t.Organizations)
                .WithOne(o => o.Tenant)
                .HasForeignKey(o => o.TenantId);


            modelBuilder.Entity<Tenants>()
                .Property(u => u.Type)
                .HasConversion<string>();

            modelBuilder.Entity<Users>()
                .Property(u => u.AccessLevel)
                .HasConversion<string>();

            modelBuilder.Entity<Organizations>()
                .Property(u => u.Level)
                .HasConversion<string>();
        }
    }
}
