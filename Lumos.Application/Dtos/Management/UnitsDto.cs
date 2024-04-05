using Lumos.Application.Dtos.Management.Tenants;
using Lumos.Data.Enums;
using Lumos.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Lumos.Application.Dtos.Management
{
    public class UnitsDto : LumosBaseModel
    {
        [Required(ErrorMessage = "O nome da unidade é obrigatório.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O nível é obrigatório.")]
        public ELevelOrganization Level { get; set; }
        public string LevelName => Level.GetDisplayNameLevel();

        [Required(ErrorMessage = "O CPF/CNPJ é obrigatório.")]
        public string CpfCnpj { get; set; }

        public long TenantId { get; set; }

        public TenantsDto Tenant { get; set; }

        public List<UsersDto> Users { get; set; }
    }
}
