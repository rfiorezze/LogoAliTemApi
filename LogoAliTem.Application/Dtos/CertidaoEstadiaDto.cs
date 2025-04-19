using LogoAliTem.Domain;

namespace LogoAliTem.Application.Dtos
{
    public class CertidaoEstadiaDto
    {
        public int CalculoEstadiaId { get; set; }

        public string Placa { get; set; }
        public string RNTRC { get; set; }
        public string NomeMotorista { get; set; }
        public string CpfCnpjMotorista { get; set; }
        public string EmailMotorista { get; set; }
        public string TelefoneMotorista { get; set; }
        public string CepMotorista { get; set; }
        public string LogradouroMotorista { get; set; }
        public string NumeroMotorista { get; set; }
        public string ComplementoMotorista { get; set; }
        public string BairroMotorista { get; set; }
        public string CidadeMotorista { get; set; }
        public string EstadoMotorista { get; set; }

        public string CpfCnpjLocalCarga { get; set; }
        public string NomeLocalCarga { get; set; }
        public string EmailLocalCarga { get; set; }
        public string TelefoneLocalCarga { get; set; }
        public string CepLocalCarga { get; set; }
        public string LogradouroLocalCarga { get; set; }
        public string NumeroLocalCarga { get; set; }
        public string ComplementoLocalCarga { get; set; }
        public string BairroLocalCarga { get; set; }
        public string CidadeLocalCarga { get; set; }
        public string EstadoLocalCarga { get; set; }

        public string CpfCnpjLocalDescarga { get; set; }
        public string NomeLocalDescarga { get; set; }
        public string EmailLocalDescarga { get; set; }
        public string TelefoneLocalDescarga { get; set; }
        public string CepLocalDescarga { get; set; }
        public string LogradouroLocalDescarga { get; set; }
        public string NumeroLocalDescarga { get; set; }
        public string ComplementoLocalDescarga { get; set; }
        public string BairroLocalDescarga { get; set; }
        public string CidadeLocalDescarga { get; set; }
        public string EstadoLocalDescarga { get; set; }

        public string CpfCnpjContratante { get; set; }
        public string NomeContratante { get; set; }
        public string EmailContratante { get; set; }
        public string TelefoneContratante { get; set; }
        public string CepContratante { get; set; }
        public string LogradouroContratante { get; set; }
        public string NumeroContratante { get; set; }
        public string ComplementoContratante { get; set; }
        public string BairroContratante { get; set; }
        public string CidadeContratante { get; set; }
        public string EstadoContratante { get; set; }

        public string CteCiotContratante { get; set; }

        public CertidaoEstadia ToEntity(byte[] arquivo)
        {
            return new CertidaoEstadia
            {
                CalculoEstadiaId = CalculoEstadiaId,

                Placa = Placa,
                RNTRC = RNTRC,
                NomeMotorista = NomeMotorista,
                CpfCnpjMotorista = CpfCnpjMotorista,
                EmailMotorista = EmailMotorista,
                TelefoneMotorista = TelefoneMotorista,
                CepMotorista = CepMotorista,
                LogradouroMotorista = LogradouroMotorista,
                NumeroMotorista = NumeroMotorista,
                ComplementoMotorista = ComplementoMotorista,
                BairroMotorista = BairroMotorista,
                CidadeMotorista = CidadeMotorista,
                EstadoMotorista = EstadoMotorista,

                CpfCnpjLocalCarga = CpfCnpjLocalCarga,
                NomeLocalCarga = NomeLocalCarga,
                EmailLocalCarga = EmailLocalCarga,
                TelefoneLocalCarga = TelefoneLocalCarga,
                CepLocalCarga = CepLocalCarga,
                LogradouroLocalCarga = LogradouroLocalCarga,
                NumeroLocalCarga = NumeroLocalCarga,
                ComplementoLocalCarga = ComplementoLocalCarga,
                BairroLocalCarga = BairroLocalCarga,
                CidadeLocalCarga = CidadeLocalCarga,
                EstadoLocalCarga = EstadoLocalCarga,

                CpfCnpjLocalDescarga = CpfCnpjLocalDescarga,
                NomeLocalDescarga = NomeLocalDescarga,
                EmailLocalDescarga = EmailLocalDescarga,
                TelefoneLocalDescarga = TelefoneLocalDescarga,
                CepLocalDescarga = CepLocalDescarga,
                LogradouroLocalDescarga = LogradouroLocalDescarga,
                NumeroLocalDescarga = NumeroLocalDescarga,
                ComplementoLocalDescarga = ComplementoLocalDescarga,
                BairroLocalDescarga = BairroLocalDescarga,
                CidadeLocalDescarga = CidadeLocalDescarga,
                EstadoLocalDescarga = EstadoLocalDescarga,

                CpfCnpjContratante = CpfCnpjContratante,
                NomeContratante = NomeContratante,
                EmailContratante = EmailContratante,
                TelefoneContratante = TelefoneContratante,
                CepContratante = CepContratante,
                LogradouroContratante = LogradouroContratante,
                NumeroContratante = NumeroContratante,
                ComplementoContratante = ComplementoContratante,
                BairroContratante = BairroContratante,
                CidadeContratante = CidadeContratante,
                EstadoContratante = EstadoContratante,
                CteCiotContratante = CteCiotContratante,
                Arquivo = arquivo
            };
        }
    }
}
