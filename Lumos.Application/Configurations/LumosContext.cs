using Microsoft.EntityFrameworkCore;

namespace Lumos.Application.Configurations
{
    public class LumosContext : DbContext
    {
        public LumosContext(DbContextOptions<LumosContext> options) : base(options)
        {
        }
    }
}
