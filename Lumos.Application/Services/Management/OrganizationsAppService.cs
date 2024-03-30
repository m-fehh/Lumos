using Lumos.Application.Interfaces.Management;
using Lumos.Application.Repositories;
using Lumos.Data.Models.Management;

namespace Lumos.Application.Services.Management
{
    public class OrganizationsAppService : LumosAppServiceBase<Organizations>, IOrganizationsAppService
    {
        public OrganizationsAppService(LumosSession session, IRepository<Organizations> repository) : base(session, repository)
        {
        }
    }
}
