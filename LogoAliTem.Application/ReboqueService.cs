using AutoMapper;
using LogoAliTem.Application.Dtos;
using LogoAliTem.Application.Interfaces;
using LogoAliTem.Domain;
using LogoAliTem.Domain.Helpers;
using LogoAliTem.Persistence.Interfaces;
using System;
using System.Text;
using System.Threading.Tasks;

namespace LogoAliTem.Application;

public class ReboqueService : IReboqueService
{
    private const string EnderecoEscritorio = "R. Um, Alameda Ipê Amarelo, 153, Esmeraldas - MG, 35740-000";
    private readonly IBaseRepository _baseRepository;
    private readonly IMapper _mapper;
    private readonly ILocalizacaoService _localizacaoService;
    private readonly IEmailService _emailService;
    private readonly ICalculoReboqueRepository _calculoReboqueRepository;
    private readonly IReboqueSolicitacaoRepository _reboqueSolicitacaoRepository;

    public ReboqueService(
        IBaseRepository baseRepository,
        IMapper mapper,
        ILocalizacaoService localizacaoService,
        IEmailService emailService,
        ICalculoReboqueRepository calculoReboqueRepository,
        IReboqueSolicitacaoRepository reboqueSolicitacaoRepository)
    {
        _baseRepository = baseRepository;
        _mapper = mapper;
        _localizacaoService = localizacaoService;
        _emailService = emailService;
        _calculoReboqueRepository = calculoReboqueRepository;
        _reboqueSolicitacaoRepository = reboqueSolicitacaoRepository;
    }


    public async Task<double?> CalcularValorAsync(ReboqueCalculoDto request)
    {
        try
        {
            double distancia1 = await _localizacaoService.CalcularDistanciaKm(EnderecoEscritorio, request.LocalRetirada);
            double distancia2 = await _localizacaoService.CalcularDistanciaKm(request.LocalRetirada, request.LocalDestino);
            double distancia3 = await _localizacaoService.CalcularDistanciaKm(request.LocalDestino, EnderecoEscritorio);

            double distanciaTotal = distancia1 + distancia2 + distancia3;

            (double valorPorKm, double arrancada) = request.TipoVeiculo switch
            {
                "Leve" => (3.80, 149.90),
                "Utilitario" => (4.50, 169.90),
                "Semi-Pesado" => (5.00, 275.90),
                _ => throw new ArgumentException("Tipo de veículo inválido")
            };

            double valorFinal = distanciaTotal <= 40 ? arrancada : distanciaTotal * valorPorKm;

            // Registra o cálculo
            await _calculoReboqueRepository.RegistrarCalculoAsync(_mapper.Map<CalculoReboque>(request));

            // Notifica o cliente
            await _emailService.EnviarEmailAsync(
                emailDestino: "servicoslogoalitem@gmail.com",
                assunto: "Novo Cálculo de Reboque",
                corpo: $@"
                <h3>Detalhes do Cálculo de Reboque:</h3>
                <p><b>Tipo de Veículo:</b> {request.TipoVeiculo}</p>
                <p><b>Local de Retirada:</b> {request.LocalRetirada}</p>
                <p><b>Local de Destino:</b> {request.LocalDestino}</p>
                <p><b>Distância Total:</b> {distanciaTotal:F2} km</p>
                <p><b>Valor Estimado:</b> R$ {valorFinal:F2}</p>
                <p><b>Data do Cálculo:</b> {TimeZoneHelper.ConvertToBrasilia(DateTime.UtcNow):dd/MM/yyyy HH:mm}</p>
            ",
                copiaPara: null,
                anexo: null,
                nomeArquivoAnexo: null
            );

            return valorFinal;
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
            corpo.AppendLine($"<p><b>Placa do Veículo:</b> {request.Placa}</p>");
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

    public async Task<IndicadoresReboqueDto> ObterIndicadoresAsync()
    {
        var totalCalculos = await _calculoReboqueRepository.GetTotalCalculosAsync();
        var totalContratacoes = await _reboqueSolicitacaoRepository.GetTotalSolicitacoesAsync();

        return new IndicadoresReboqueDto
        {
            TotalCalculos = totalCalculos,
            TotalContratacoes = totalContratacoes
        };
    }

}