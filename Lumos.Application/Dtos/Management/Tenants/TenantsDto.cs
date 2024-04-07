using Lumos.Data.Enums;
using Lumos.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Lumos.Application.Dtos.Management.Tenants
{
    public class TenantsDto : LumosBaseModel
    {
        [Required(ErrorMessage = "O nome do inquilino é obrigatório.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O email do inquilino é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email fornecido não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O tipo de inquilino é obrigatório.")]
        public ETenantType Type { get; set; }

        public string TypeName => Type.GetDisplayName();

        [Required(ErrorMessage = "A cidade do inquilino é obrigatória.")]
        public string City { get; set; }

        [Required(ErrorMessage = "O estado do inquilino é obrigatório.")]
        public string State { get; set; }

        public List<UnitsDto> Units { get; set; }
    }
}
