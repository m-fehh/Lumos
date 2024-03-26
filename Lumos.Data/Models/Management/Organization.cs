using System.ComponentModel.DataAnnotations;

namespace Lumos.Data.Models.Management
{
    public class Organization : LumosBaseModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public List<User> Users { get; set; } = new List<User>();

        public Tenant Tenant { get; set; }
    }
}
