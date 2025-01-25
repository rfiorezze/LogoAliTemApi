using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LogoAliTem.Application.Dtos
{
    public class UserUpdateDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O nome completo é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome completo deve ter no máximo 100 caracteres")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório")]
        [Phone(ErrorMessage = "Telefone inválido")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [RegularExpression("^[0-9]{11}$", ErrorMessage = "O CPF deve conter exatamente 11 números")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O sexo é obrigatório.")]
        [RegularExpression("^[MF]$", ErrorMessage = "O sexo deve ser 'M' (Masculino) ou 'F' (Feminino).")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        [DataType(DataType.Date, ErrorMessage = "Data de nascimento inválida")]
        public DateTime DataNascimento { get; set; }

        public IEnumerable<string> UserRoles { get; set; }
        public string Token { get; set; }
    }
}
