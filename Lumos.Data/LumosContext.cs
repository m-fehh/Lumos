using Lumos.Data.Models.Management;
using Microsoft.EntityFrameworkCore;

namespace Lumos.Data
{
    public class LumosContext : DbContext
    {
        public DbSet<Tenant> Tenant { get; set; }

        public LumosContext(DbContextOptions<LumosContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { }
    }
}
