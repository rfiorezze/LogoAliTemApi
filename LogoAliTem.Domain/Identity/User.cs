using LogoAliTem.Domain.Enum;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LogoAliTem.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O nome completo deve ter no máximo 50 caracteres.")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [MaxLength(11, ErrorMessage = "O telefone deve ter no máximo 11 caracteres.")]
        [RegularExpression("^[0-9]{10,11}$", ErrorMessage = "O telefone deve conter apenas números com 10 ou 11 dígitos.")]
        public string Telefone { get; set; }

        [MaxLength(11, ErrorMessage = "O CPF deve ter no máximo 11 caracteres.")]
        [RegularExpression("^[0-9]{11}$", ErrorMessage = "O CPF deve conter exatamente 11 números.")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O sexo é obrigatório.")]
        [MaxLength(1, ErrorMessage = "O sexo deve ser representado por um único caractere.")]
        [RegularExpression("^[MF]$", ErrorMessage = "O sexo deve ser 'M' (Masculino) ou 'F' (Feminino).")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        public DateTime DataNascimento { get; set; }

        public string ImagemURL { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}