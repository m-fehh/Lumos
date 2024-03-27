using Lumos.Data.Models.Management;

namespace Lumos.Application.Interfaces.Management
{
    public interface IUserAppService : ITransientDependency
    {
        Task<User?> ValidateUserCredentials(string email, string password);
    }
}
