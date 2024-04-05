using Lumos.Application.Configurations;
using Lumos.Application.Models;
using Lumos.Data.Models.Management;

namespace Lumos.Application.Interfaces.Management
{
    public interface IUnitsAppService : ITransientDependency
    {
        Task CreateAsync(Units entity);
    }
}
