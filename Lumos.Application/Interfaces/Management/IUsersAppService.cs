using Lumos.Application.Configurations;
using Lumos.Application.Models;
using Lumos.Data.Models.Management;

namespace Lumos.Application.Interfaces.Management
{
    public interface IUsersAppService : ITransientDependency
    {
        Task<PaginationResult<Users>> GetAllPaginatedAsync(UserDataTableParams dataTableParams, long? tenantId, List<long> organizationId, bool isHost);
        Task<Users?> ValidateUserCredentials(string email, string password);
        Task CreateAsync(Users entity);
    }
}
