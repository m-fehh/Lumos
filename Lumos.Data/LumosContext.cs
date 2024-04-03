using Lumos.Data.Models;
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
            ConfigureEnumerations(modelBuilder);

            // Configuração da relação entre Users e Address
            modelBuilder.Entity<Users>()
                .HasOne(u => u.Address)
                .WithOne(a => a.User)
                .HasForeignKey<Address>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuração da relação entre Users e Organizations
            modelBuilder.Entity<Users>()
                .HasMany(u => u.Organizations)
                .WithMany(o => o.Users);

            // Configuração da relação entre Organizations e Tenants
            modelBuilder.Entity<Organizations>()
                .HasOne(o => o.Tenant)
                .WithMany(t => t.Organizations)
                .HasForeignKey(o => o.TenantId)
                .OnDelete(DeleteBehavior.Restrict); 

            // Configuração da relação entre Users e Tenants
            modelBuilder.Entity<Users>()
                .HasOne(u => u.Tenant)
                .WithMany(t => t.Users)
                .HasForeignKey(u => u.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuração da relação entre Tenants e Users
            modelBuilder.Entity<Tenants>()
                .HasMany(t => t.Users)
                .WithOne(u => u.Tenant)
                .HasForeignKey(u => u.TenantId);

            // Configuração da relação entre Tenants e Organizations
            modelBuilder.Entity<Tenants>()
                .HasMany(t => t.Organizations)
                .WithOne(o => o.Tenant)
                .HasForeignKey(o => o.TenantId);
        }

        private void ConfigureEnumerations(ModelBuilder modelBuilder)
        {
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
