using Lumos.Application.Configurations;
using Lumos.Application.Dtos.Management.Tenants;
using Lumos.Application.Services.Management;
using Lumos.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Lumos.Application.Dtos.Management
{
    public class UsersDto : LumosBaseModel
    {
        [Required(ErrorMessage = "O email é obrigatório.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Password { get; set; }
        public string DecryptedPassword { get; set; }

        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        public string Cpf { get; set; }

        public long TenantId { get; set; }
        public TenantsDto Tenant { get; set; }
        public List<UnitsDto> Units { get; set; }
        public string SerializedUnitsList { get; set; }

        public void DecryptPassword()
        {
            var encryptionService = new AesEncryptionService();
            DecryptedPassword = encryptionService.Decrypt(Password);
        }
    }
}
