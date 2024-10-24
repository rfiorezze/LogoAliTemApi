using LogoAliTem.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LogoAliTem.Application;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;
    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task EnviarEmailAsync(string emailDestino, string assunto, string corpo, string copiaPara, byte[] anexo, string nomeArquivoAnexo)
    {
        try
        {
            // Configurar o cliente SMTP (Gmail, por exemplo)
            using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.Credentials = new NetworkCredential("contato@logoalitem.com.br", _config["SenhaAppGmail"]); // Use a Senha de Aplicativo aqui
                smtpClient.EnableSsl = true;

                // Criar a mensagem de e-mail
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("contato@logoalitem.com.br"),
                    Subject = assunto,
                    Body = corpo,
                    IsBodyHtml = true // Se o corpo for em HTML
                };

                // Adicionar destinatários
                mailMessage.To.Add(emailDestino);

                // Adicionar cópia, se fornecida
                if (!string.IsNullOrEmpty(copiaPara))
                {
                    mailMessage.CC.Add(copiaPara);
                }

                // Adicionar o anexo
                if (anexo != null && anexo.Length > 0)
                {
                    using (var stream = new MemoryStream(anexo))
                    {
                        var attachment = new Attachment(stream, nomeArquivoAnexo);
                        mailMessage.Attachments.Add(attachment);

                        // Enviar o e-mail
                        await smtpClient.SendMailAsync(mailMessage);
                    }
                }
                else
                {
                    // Enviar o e-mail
                    await smtpClient.SendMailAsync(mailMessage);
                }

            }
        }
        catch (Exception ex)
        {
            // Lidar com exceções
            throw new Exception("Erro ao enviar e-mail: " + ex.Message);
        }
    }
}