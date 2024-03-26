using System.ComponentModel.DataAnnotations;

namespace Lumos.Data.Models.Management
{
    public class Tenant : LumosBaseModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public List<User> Users { get; set; } = new List<User>();

        public List<Organization> Organizations { get; set; } = new List<Organization>();
    }
}
