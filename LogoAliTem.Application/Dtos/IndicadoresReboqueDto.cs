using System;

namespace LogoAliTem.Application.Dtos;

public class IndicadoresReboqueDto
{
    public int TotalCalculos { get; set; }
    public int TotalContratacoes { get; set; }

    public double TaxaConversao => TotalCalculos == 0 
        ? 0 
        : Math.Round((double)TotalContratacoes / TotalCalculos * 100, 2);
}