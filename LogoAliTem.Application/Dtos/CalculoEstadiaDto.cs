using LogoAliTem.Domain;
using System;

namespace LogoAliTem.Application.Dtos
{
    public class CalculoEstadiaDto
    {
        public DateTime DataChegada { get; set; }
        public TimeSpan HoraChegada { get; set; }
        public DateTime DataSaida { get; set; }
        public TimeSpan HoraSaida { get; set; }
        public double CapacidadeCargaVeiculo { get; set; }
        public double ValorCalculado { get; set; }

        public CalculoEstadia ToEntity()
        {
            return new CalculoEstadia
            {
                DataChegada = DataChegada,
                HoraChegada = HoraChegada,
                DataSaida = DataSaida,
                HoraSaida = HoraSaida,
                CapacidadeCargaVeiculo = CapacidadeCargaVeiculo,
                ValorCalculado = ValorCalculado
            };
        }
    }
}
