using Lumos.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lumos.Data.Models.Management
{
    [Table("tbTenants")]
    public class Tenants : LumosBaseModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public ETenantType Type { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }
        public virtual List<Units> Units { get; set; }
    }
}

