using System.Threading.Tasks;

namespace LogoAliTem.Application.Interfaces;

public interface IEmailService
{
    Task EnviarEmailAsync(string emailDestino, string assunto, string corpo, string copiaPara = null, byte[] anexo = null, string nomeArquivoAnexo = null);
}