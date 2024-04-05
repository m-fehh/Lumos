using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lumos.Data.Models.Management
{
    [Table("tbUsers")]
    public class Users : LumosBaseModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Cpf { get; set; }

        public long TenantId { get; set; }
        public virtual Tenants Tenant { get; set; }
        public virtual IList<Units> Units { get; set; }
    }
}
