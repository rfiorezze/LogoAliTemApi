using LogoAliTem.Domain.Enum;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace LogoAliTem.Domain.Identity;
public class User : IdentityUser<int>
{
    public string NomeCompleto { get; set; }
    public string Telefone { get; set; }
    public string ImagemURL { get; set; }
    public Funcao Funcao { get; set; }
    public IEnumerable<UserRole> UserRoles { get; set; }
}