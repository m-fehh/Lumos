using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lumos.Data.Models.Management
{
    [Table("tbTenants")]
    public class Tenant : LumosBaseModel
    {
        [Required(ErrorMessage = "O nome do tenant é obrigatório.")]
        public string Name { get; set; }

        public string Description { get; set; }

        public List<User> Users { get; set; }

        public List<Organization> Organizations { get; set; }

        public Tenant()
        {
            Users = new List<User>();
            Organizations = new List<Organization>();
        }
    }
}

