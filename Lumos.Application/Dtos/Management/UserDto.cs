using Lumos.Application.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lumos.Application.Dtos.Management
{
    public class UserDto
    {
        #region Login

        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome de usuário deve ter entre 3 e 50 caracteres.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email fornecido não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "O nível de acesso é obrigatório.")]
        public EAccessLevel AccessLevel { get; set; }

        #endregion

        #region Identificação

        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "O CPF fornecido não é válido.")]
        public string Cpf { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "O gênero é obrigatório.")]
        public string Gender { get; set; }

        #endregion

        #region Endereço

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        public AddressDto Address { get; set; }

        #endregion

        #region Contato

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "O método de contato é obrigatório.")]
        public string ContactMethod { get; set; }

        #endregion
    }

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
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "O CEP fornecido não é válido. Use o formato 99999-999.")]
        public string ZipCode { get; set; }
    }
}
