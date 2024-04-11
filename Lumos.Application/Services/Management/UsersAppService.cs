using Lumos.Application.Configurations;
using Lumos.Application.Consts;
using Lumos.Application.Interfaces.Management;
using Lumos.Application.Repositories;
using Lumos.Data.Models.Management;
using System.Security.Cryptography;
using System.Text;

namespace Lumos.Application.Services.Management
{
    public class UsersAppService : LumosAppServiceBase<Users>, IUsersAppService
    {
        public UsersAppService(LumosSession session, IRepository<Users> repository) : base(session, repository)
        {
        }

        public async Task<Users?> ValidateUserCredentials(string email, string password)
        {
            if (HostConfigurationConst.HostLogin.Equals(email) && HostConfigurationConst.HostPassword.Equals(password)) 
            {
                return new Users
                {
                    FullName = "Admin Master"
                };
            }
            else
            {
                var user = (await _repository.GetAllAsync()).FirstOrDefault(u => u.Email == email);

                var encryptionService = new AesEncryptionService();

                if (user != null && encryptionService.Decrypt(password) == user.Password)
                {
                    return user;
                }
            }

            return null;
        }
    }
}
