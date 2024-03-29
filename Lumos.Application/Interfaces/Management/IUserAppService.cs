using Lumos.Application.Configurations;
using Lumos.Application.Models;
using Lumos.Data.Models.Management;

namespace Lumos.Application.Interfaces.Management
{
    public interface IUserAppService : ITransientDependency
    {
        Task<User?> ValidateUserCredentials(string email, string password);
        Task<PaginationResult<User>> GetAllPaginatedAsync(UserDataTableParams dataTableParams);
        string HashPassword(string password);
        Task CreateAsync(User entity);
    }
}
