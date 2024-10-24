namespace LogoAliTem.Application.Dtos
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string NomeCompleto { get; set; }
        public string Telefone { get; set; }
        public string Funcao { get; set; }
        public string Token { get; set; }
    }
}