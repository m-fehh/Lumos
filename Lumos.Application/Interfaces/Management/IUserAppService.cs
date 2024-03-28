using Lumos.Data.Models.Management;

namespace Lumos.Application.Interfaces.Management
{
    public interface IUserAppService : ITransientDependency
    {
        Task<User?> ValidateUserCredentials(string email, string password);
        Task<List<User>> GetAllPaginatedAsync(int pageNumber, int pageSize);
        string HashPassword(string password);
        Task CreateAsync(User entity);
    }
}
