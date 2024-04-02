using Lumos.Application.Enums;
using Lumos.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lumos.Data.Models.Management
{
    [Table("tbOrganizations")]
    public class Organizations : LumosBaseModel
    {
        [Required(ErrorMessage = "O nome da organização é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da organização deve ter no máximo 100 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O nível é obrigatório.")]
        public ELevelOrganization Level { get; set; }

        [Required(ErrorMessage = "O CPF/CNPJ é obrigatório.")]
        [StringLength(18, MinimumLength = 11, ErrorMessage = "O CPF/CNPJ deve ter entre 11 e 18 caracteres.")]
        public string CpfCnpj { get; set; }
        public List<Users> Users { get; set; }
        public long TenantId { get; set; }

        [ForeignKey("TenantId")]
        public Tenants Tenant { get; set; }

        public Organizations()
        {
            Users = new List<Users>();
        }
    }
}
