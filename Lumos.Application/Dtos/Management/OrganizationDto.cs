using Lumos.Application.Dtos.Management.Tenant;
using Lumos.Data.Enums;
using Lumos.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Lumos.Application.Dtos.Management
{
    public class OrganizationDto : LumosBaseModel
    {
        [Required(ErrorMessage = "O nome da organização é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da organização deve ter no máximo 100 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O nível é obrigatório.")]
        public ELevelOrganization Level { get; set; }
        public string LevelName => Level.GetDisplayNameLevel();

        [Required(ErrorMessage = "O CPF/CNPJ é obrigatório.")]
        [StringLength(18, MinimumLength = 11, ErrorMessage = "O CPF/CNPJ deve ter entre 11 e 18 caracteres.")]
        public string CpfCnpj { get; set; }

        public long? TenantId { get; set; }

        public TenantDto Tenant { get; set; }

        public List<UserDto> Users { get; set; }

        public OrganizationDto()
        {
            Users = new List<UserDto>();
        }
    }
}
