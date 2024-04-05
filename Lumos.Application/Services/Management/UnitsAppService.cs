using Lumos.Application.Interfaces.Management;
using Lumos.Application.Repositories;
using Lumos.Data.Models.Management;

namespace Lumos.Application.Services.Management
{
    public class UnitsAppService : LumosAppServiceBase<Units>, IUnitsAppService
    {
        public UnitsAppService(LumosSession session, IRepository<Units> repository) : base(session, repository)
        {
        }
    }
}
