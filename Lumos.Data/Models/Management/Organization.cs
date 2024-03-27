using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lumos.Data.Models.Management
{
    [Table("tbOrganizations")]
    public class Organization : LumosBaseModel
    {
        [Required(ErrorMessage = "O nome da organização é obrigatório.")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Cnpj { get; set; }
        public List<User> Users { get; set; }

        public long TenantId { get; set; }
        public Tenant Tenant { get; set; }

        public Organization()
        {
            Users = new List<User>();
        }
    }
}
