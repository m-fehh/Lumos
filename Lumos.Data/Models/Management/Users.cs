using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lumos.Application.Enums;

namespace Lumos.Data.Models.Management
{
    [Table("tbUsers")]
    public class Users : LumosBaseModel
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

        [Required(ErrorMessage = "O CPF é obrigatória.")]
        [StringLength(14, ErrorMessage = "O CPF deve conter no máximo 11 caracteres.")]
        public string Cpf { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "O gênero é obrigatório.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "O nível de acesso é obrigatório.")]
        public EAccessLevel AccessLevel { get; set; }

        public long AddressId { get; set; }
        [ForeignKey("AddressId")]

        public Address Address { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "O método de contato é obrigatório.")]
        public string ContactMethod { get; set; }

        public long TenantId { get; set; }

        [ForeignKey("TenantId")]
        public Tenants Tenant { get; set; }

        public long OrganizationId { get; set; }

        [ForeignKey("OrganizationId")]
        public Organizations Organization { get; set; }

        // Método para verificar se o usuário pertence a um tenant específico
        public bool BelongsToTenant(Tenants tenant)
        {
            return Tenant?.Id == tenant.Id;
        }
    }
}