using AutoMapper;
using LogoAliTem.Application.Dtos;
using LogoAliTem.Application.Interfaces;
using LogoAliTem.Domain;
using LogoAliTem.Persistence.Interfaces;
using System;
using System.Text;
using System.Threading.Tasks;

public class ReboqueService : IReboqueService
{
    private const string EnderecoEscritorio = "7RGX+Q4W Esmeraldas, MG";
    private readonly IBaseRepository _baseRepository;
    private readonly IMapper _mapper;
    private readonly ILocalizacaoService _localizacaoService;
    private readonly IEmailService _emailService;

    public ReboqueService(IBaseRepository baseRepository, IMapper mapper, ILocalizacaoService localizacaoService, IEmailService emailService)
    {
        _baseRepository = baseRepository;
        _mapper = mapper;
        _localizacaoService = localizacaoService;
        _emailService = emailService;
    }

    public async Task<double?> CalcularValorAsync(ReboqueCalculoDto request)
    {
        try
        {
            double distancia1 = await _localizacaoService.CalcularDistanciaKm(EnderecoEscritorio, request.LocalRetirada);
            double distancia2 = await _localizacaoService.CalcularDistanciaKm(request.LocalRetirada, request.LocalDestino);
            double distancia3 = await _localizacaoService.CalcularDistanciaKm(request.LocalDestino, EnderecoEscritorio);

            double distanciaTotal = distancia1 + distancia2 + distancia3;

            return distanciaTotal <= 40 ? 140 : distanciaTotal * 4;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao calcular valor do reboque: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> ContratarReboqueAsync(ReboqueSolicitacaoDto request)
    {
        try
        {
            var model = _mapper.Map<ReboqueSolicitacao>(request);

            if (model == null)
                throw new Exception("Falha ao mapear a solicitação de reboque.");

            _baseRepository.Add(model);
            bool sucesso = await _baseRepository.SaveChangesAsync();

            if (sucesso)
            {
                await EnviarEmailReboque(request);
            }

            return sucesso;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao contratar reboque: {ex.Message}");
            return false;
        }
    }

    private async Task EnviarEmailReboque(ReboqueSolicitacaoDto request)
    {
        try
        {
            string assunto = "Nova Solicitação de Reboque";
            StringBuilder corpo = new StringBuilder();

            corpo.AppendLine("<h3>Detalhes da Solicitação de Reboque:</h3>");
            corpo.AppendLine($"<p><b>Tipo de Veículo:</b> {request.TipoVeiculo}</p>");
            corpo.AppendLine($"<p><b>Local de Retirada:</b> {request.LocalRetirada}</p>");
            corpo.AppendLine($"<p><b>Local de Destino:</b> {request.LocalDestino}</p>");
            corpo.AppendLine($"<p><b>Valor Estimado:</b> R$ {request.ValorEstimado:F2}</p>");
            corpo.AppendLine($"<p><b>Data da Solicitação:</b> {DateTime.UtcNow:dd/MM/yyyy HH:mm}</p>");

            await _emailService.EnviarEmailAsync(
                emailDestino: "contato@logoalitem.com.br",
                assunto: assunto,
                corpo: corpo.ToString(),
                copiaPara: "",
                anexo: null,
                nomeArquivoAnexo: ""
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao enviar e-mail de reboque: {ex.Message}");
        }
    }
}