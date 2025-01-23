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

        public IEnumerable<string> UserRoles { get; set; }
        public string Token { get; set; }
    }
}
