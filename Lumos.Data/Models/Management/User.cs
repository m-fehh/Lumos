using System.ComponentModel.DataAnnotations;

namespace Lumos.Data.Models.Management
{
    public class User : LumosBaseModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public Tenant Tenant { get; set; }

        // Método para verificar se o usuário pertence a um tenant específico
        public bool BelongsToTenant(Tenant tenant)
        {
            return Tenant?.Id == tenant.Id;
        }
    }
}
