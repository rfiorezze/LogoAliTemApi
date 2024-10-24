using LogoAliTem.Application;
using LogoAliTem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LogoAliTem.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly IEmailService _emailService;

    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("enviar-email")]
    public async Task<IActionResult> EnviarEmail(
        [FromForm] IFormFile arquivo,
        [FromForm] string emailDestino,
        [FromForm] string assunto,
        [FromForm] string corpo,
        [FromForm] string copiaPara)
    {
        byte[] anexo = null;
        string nomeArquivo = null;

        if (arquivo != null && arquivo.Length > 0)
        {
            using (var memoryStream = new MemoryStream())
            {
                await arquivo.CopyToAsync(memoryStream);
                anexo = memoryStream.ToArray();
                nomeArquivo = arquivo.FileName;
            }
        }

        try
        {
            await _emailService.EnviarEmailAsync(emailDestino, assunto, corpo, copiaPara, anexo, nomeArquivo);

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao enviar e-mail: {ex.Message}");
        }
    }
}
