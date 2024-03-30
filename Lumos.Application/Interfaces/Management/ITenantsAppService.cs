using Lumos.Application.Configurations;
using Lumos.Application.Models;
using Lumos.Data.Models.Management;

namespace Lumos.Application.Interfaces.Management
{
    public interface ITenantsAppService : ITransientDependency
    {
        Task<PaginationResult<Tenants>> GetAllPaginatedAsync(UserDataTableParams dataTableParams);
        Task<TId> InsertAndGetIdAsync<TId>(Tenants entity);
    }
}
