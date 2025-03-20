using AutoMapper;
using LogoAliTem.Application.Dtos;
using LogoAliTem.Application.Interfaces;
using LogoAliTem.Domain;
using LogoAliTem.Domain.Helpers;
using LogoAliTem.Persistence.Interfaces;
using System;
using System.Text;
using System.Threading.Tasks;

public class ReboqueService : IReboqueService
{
    private const string EnderecoEscritorio = "R. Um, Alameda Ipê Amarelo, 153, Esmeraldas - MG, 35740-000";
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

            // Define o valor por km baseado no tipo de veículo
            double valorPorKm = request.TipoVeiculo switch
            {
                "Leve" => 4.20,
                "Utilitario" => 4.70,
                "Semi-Pesado" => 5.20,
                _ => throw new ArgumentException("Tipo de veículo inválido") // Lança erro se o tipo for desconhecido
            };

            return distanciaTotal <= 40 ? 149.90 : distanciaTotal * valorPorKm;
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

            // Converte a data para o horário de Brasília
            DateTime dataBrasilia = TimeZoneHelper.ConvertToBrasilia(DateTime.UtcNow);

            corpo.AppendLine("<h3>Detalhes da Solicitação de Reboque:</h3>");
            corpo.AppendLine($"<p><b>Tipo de Veículo:</b> {request.TipoVeiculo}</p>");
            corpo.AppendLine($"<p><b>Local de Retirada:</b> {request.LocalRetirada}</p>");
            corpo.AppendLine($"<p><b>Local de Destino:</b> {request.LocalDestino}</p>");
            corpo.AppendLine($"<p><b>Valor Estimado:</b> R$ {request.ValorEstimado:F2}</p>");
            corpo.AppendLine($"<p><b>Data da Solicitação:</b> {dataBrasilia:dd/MM/yyyy HH:mm}</p>");

            await _emailService.EnviarEmailAsync(
                emailDestino: "servicoslogoalitem@gmail.com",
                assunto: assunto,
                corpo: corpo.ToString(),
                copiaPara: "contato@logoalitem.com.br",
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