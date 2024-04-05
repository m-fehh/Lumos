using Lumos.Application.Consts;
using Lumos.Application.Interfaces.Management;
using Lumos.Application.Repositories;
using Lumos.Data.Models.Management;

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
                    FullName = "HOST_ACCESS"
                };
            }
            else
            {
                var user = (await _repository.GetAllAsync()).FirstOrDefault(u => u.Email == email);

                if (user != null && VerifyPassword(password, user.Password))
                {
                    return user;
                }
            }
            // Retorna null se as credenciais forem inválidas
            return null;
        }




        #region METHODS PASSWORD
        
        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public string HashPassword(string password)
        {
            // Gera um hash seguro da senha
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
        } 

        #endregion
    }
}
