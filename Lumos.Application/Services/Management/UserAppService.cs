using Lumos.Application.Interfaces.Management;
using Lumos.Application.Repositories;
using Lumos.Data.Models.Management;

namespace Lumos.Application.Services.Management
{
    public class UserAppService : LumosAppServiceBase<User>, IUserAppService
    {
        public UserAppService(LumosSession session, IRepository<User> repository) : base(session, repository)
        {
        }

        public async Task<User?> ValidateUserCredentials(string email, string password)
        {
            var user = (await _repository.GetAllAsync()).FirstOrDefault(u => u.Email == email);

            if (user != null && VerifyPassword(password, user.PasswordHash))
            {
                return user;
            }

            // Retorna null se as credenciais forem inválidas
            return null;
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        // GERA SENHA CRÍPTOGRAFADA
        //public string HashPassword(string password)
        //{
        //    // Gera um hash seguro da senha
        //    return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
        //}
    }
}
