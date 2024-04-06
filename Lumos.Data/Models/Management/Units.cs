using Lumos.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lumos.Data.Models.Management
{
    [Table("tbUnits")]
    public class Units : LumosBaseModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public virtual ELevelOrganization Level { get; set; }

        [Required]
        public string CpfCnpj { get; set; }

        public long TenantId { get; set; }
        public virtual Tenants Tenant { get; set; }
    }
}
