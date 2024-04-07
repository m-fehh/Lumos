using Lumos.Data.Models.Management;

namespace Lumos.Application.Interfaces.Management
{
    public interface IUnitsAppService : ITransientDependency
    {
        Task CreateAsync(Units entity);
        Task<List<Units>> GetByListIdsAsync<TId>(List<TId> id);
    }
}
