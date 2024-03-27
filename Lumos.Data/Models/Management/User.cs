using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lumos.Data.Models.Management
{
    [Table("tbUsers")]
    public class User : LumosBaseModel
    {
        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome de usuário deve ter entre 3 e 50 caracteres.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email fornecido não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        public string PasswordHash { get; set; }


        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        public string FullName { get; set; }

        [StringLength(11)]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter 11 dígitos numéricos.")]
        public string Cpf { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Gender { get; set; }

        public long AddressId { get; set; }
        public Address Address { get; set; }

        public string Phone { get; set; }

        public string ProfileImageUrl { get; set; }

        public long TenantId { get; set; }
        public Tenant Tenant { get; set; }

        public long OrganizationId { get; set; }
        public Organization Organization { get; set; }

        // Método para verificar se o usuário pertence a um tenant específico
        public bool BelongsToTenant(Tenant tenant)
        {
            return Tenant?.Id == tenant.Id;
        }
    }
}
