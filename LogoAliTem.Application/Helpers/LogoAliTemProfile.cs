using AutoMapper;
using LogoAliTem.Application.Dtos;
using LogoAliTem.Domain;
using LogoAliTem.Domain.Identity;
using System;

namespace LogoAliTem.Application.Helpers;

public class LogoAliTemProfile : Profile
{
    public LogoAliTemProfile()
    {
        CreateMap<Motorista, MotoristaDto>()
            .ForMember(
            dest => dest.DataNascimento,
            opt => opt.MapFrom(src => src.DataNascimento.ToString("yyyy-MM-dd")))
            .ForMember(
            dest => dest.DataVencimentoCNH,
            opt => opt.MapFrom(src => src.DataVencimentoCNH == null ? null : Convert.ToDateTime(src.DataVencimentoCNH).ToString("yyyy-MM-dd")))
        .ReverseMap();

        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, UserLoginDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();
        CreateMap<Veiculo, VeiculoDto>().ReverseMap();
    }
}