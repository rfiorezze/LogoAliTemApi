using LogoAliTem.Application.Interfaces;
using LogoAliTem.Domain;
using LogoAliTem.Persistence.Interfaces;
using System;
using System.Threading.Tasks;

namespace LogoAliTem.Application
{
    public class EstadiaService : IEstadiaService
    {
        private readonly ICalculoEstadiaRepository _calculoRepository;
        private readonly ICertidaoEstadiaRepository _certidaoRepository;
        private readonly IEmailService _emailService;

        public EstadiaService(
            ICalculoEstadiaRepository calculoRepository,
            ICertidaoEstadiaRepository certidaoRepository,
            IEmailService emailService)
        {
            _calculoRepository = calculoRepository;
            _certidaoRepository = certidaoRepository;
            _emailService = emailService;
        }

        public async Task<CalculoEstadia> RegistrarCalculoAsync(CalculoEstadia entity)
        {
            return await _calculoRepository.AdicionarAsync(entity);
        }

        public async Task<CertidaoEstadia> RegistrarCertidaoAsync(CertidaoEstadia entity)
        {
            // Salva a certidão no banco
            var certidao = await _certidaoRepository.AdicionarAsync(entity);

            // Define destinatários fixos
            var destinatarios = new[] { "servicoslogoalitem@gmail.com", "contato@logoalitem.com.br" };

            // Monta o assunto
            var assunto = $"📄 Nova Certidão de Estadia - Placa: {entity.Placa}";

            // Monta o corpo do e-mail
            var corpo = $@"
                            <p>Uma nova certidão de estadia foi registrada com sucesso.</p>

                            <h4>📌 Informações principais:</h4>
                            <ul>
                                <li><strong>Placa:</strong> {entity.Placa}</li>
                                <li><strong>Nome do Motorista:</strong> {entity.NomeMotorista}</li>
                                <li><strong>CPF/CNPJ do Motorista:</strong> {entity.CpfCnpjMotorista}</li>
                                <li><strong>Local de Carga:</strong> {entity.NomeLocalCarga}</li>
                                <li><strong>Local de Descarga:</strong> {entity.NomeLocalDescarga}</li>
                                <li><strong>Data:</strong> {DateTime.Now:dd/MM/yyyy HH:mm}</li>
                            </ul>

                            <p>Em anexo está o PDF da certidão gerada.</p>
                            <p style='font-size: 12px; color: #888;'>Este é um e-mail automático, não responda.</p>
                        ";

            // Envia o e-mail com cópia oculta para o segundo e-mail
            foreach (var email in destinatarios)
            {
                await _emailService.EnviarEmailAsync(
                    emailDestino: email,
                    assunto: assunto,
                    corpo: corpo,
                    copiaPara: null,
                    anexo: entity.Arquivo,
                    nomeArquivoAnexo: $"Certidão de estadia {entity.NomeMotorista}.pdf" 
                );
            }

            return certidao;
        }

        public async Task<(int totalCalculos, int totalCertidoes, double taxaConversao)> ObterIndicadoresAsync()
        {
            var totalCalculos = await _calculoRepository.ContarAsync();
            var totalCertidoes = await _certidaoRepository.ContarAsync();

            double taxa = 0;
            if (totalCalculos > 0)
                taxa = (double)totalCertidoes / totalCalculos * 100;

            return (totalCalculos, totalCertidoes, taxa);
        }
    }
}
