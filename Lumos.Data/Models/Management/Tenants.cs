using Lumos.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lumos.Data.Models.Management
{
    [Table("tbTenants")]
    public class Tenants : LumosBaseModel
    {
        #region Properties

        [Required(ErrorMessage = "O nome do tenant é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do tenant deve ter no máximo 100 caracteres.")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "O tipo é obrigatório.")]
        public ETenantType Type { get; set; }

        [Required(ErrorMessage = "A filial é obrigatória.")]
        [StringLength(50, ErrorMessage = "A filial deve ter no máximo 50 caracteres.")]
        public string Branch { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        [StringLength(50, ErrorMessage = "A cidade deve ter no máximo 50 caracteres.")]
        public string City { get; set; }

        [Required(ErrorMessage = "A UF é obrigatória.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "A UF deve ter 2 caracteres.")]
        public string Uf { get; set; }

        #endregion

        #region Relationships

        public List<Users> Users { get; set; }

        public List<Organizations> Organizations { get; set; }

        #endregion

        #region Constructor

        public Tenants()
        {
            Users = new List<Users>();
            Organizations = new List<Organizations>();
        }

        #endregion
    }
}

