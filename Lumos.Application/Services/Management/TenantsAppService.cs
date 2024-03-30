using Lumos.Application.Interfaces.Management;
using Lumos.Application.Repositories;
using Lumos.Data.Models.Management;

namespace Lumos.Application.Services.Management
{
    public class TenantsAppService : LumosAppServiceBase<Tenants>, ITenantsAppService
    {
        public TenantsAppService(LumosSession session, IRepository<Tenants> repository) : base(session, repository)
        {
        }
    }
}
