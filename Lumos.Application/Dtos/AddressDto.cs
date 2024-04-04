using System.ComponentModel.DataAnnotations;

namespace Lumos.Application.Dtos
{
    public class AddressDto
    {
        [Required(ErrorMessage = "O endereço é obrigatório.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "O número é obrigatório.")]
        public string Number { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        public string City { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório.")]
        public string State { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        public string ZipCode { get; set; }
    }
}
