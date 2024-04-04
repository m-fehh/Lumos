using Lumos.Application.Dtos.Management.Tenant;
using Lumos.Application.Enums;
using Lumos.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lumos.Application.Dtos.Management
{
    public class UserDto : LumosBaseModel
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

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(14, ErrorMessage = "O CPF deve conter no máximo 14 caracteres.")]
        public string Cpf { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "O gênero é obrigatório.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "O nível de acesso é obrigatório.")]
        public EAccessLevel AccessLevel { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "O método de contato é obrigatório.")]
        public string ContactMethod { get; set; }

        public long AddressId { get; set; }
        public AddressDto Address { get; set; }

        public long? TenantId { get; set; }
        public TenantDto Tenant { get; set; }
        public List<OrganizationDto> Organizations { get; set; }

        public UserDto()
        {
            Organizations = new List<OrganizationDto>();
        }
    }
}
