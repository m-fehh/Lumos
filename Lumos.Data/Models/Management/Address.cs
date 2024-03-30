using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lumos.Data.Models.Management
{
    [Table("tbAddress")]
    public class Address
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "O campo rua é obrigatório.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "O campo número é obrigatório.")]
        public string Number { get; set; }

        [Required(ErrorMessage = "O campo cidade é obrigatório.")]
        public string City { get; set; }

        [Required(ErrorMessage = "O campo estado é obrigatório.")]
        public string State { get; set; }

        [Required(ErrorMessage = "O campo CEP é obrigatório.")]
        public string ZipCode { get; set; }

        public long? UserId { get; set; }
        public Users User { get; set; }
    }
}
