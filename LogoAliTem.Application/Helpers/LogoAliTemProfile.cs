using AutoMapper;
using LogoAliTem.Application.Dtos;
using LogoAliTem.Domain.Identity;
using LogoAliTem.Domain;
using System.Collections.Generic;
using System;
using System.Linq;

public class LogoAliTemProfile : Profile
{
    public LogoAliTemProfile()
    {
        // Motorista
        CreateMap<Motorista, MotoristaDto>()
            .ForMember(
                dest => dest.DataNascimento,
                opt => opt.MapFrom(src => src.DataNascimento.ToString("yyyy-MM-dd")))
            .ForMember(
                dest => dest.DataVencimentoCNH,
                opt => opt.MapFrom(src => src.DataVencimentoCNH == null ? null : Convert.ToDateTime(src.DataVencimentoCNH).ToString("yyyy-MM-dd")))
            .ReverseMap();

        // User <-> UserDto
        CreateMap<User, UserDto>()
            .ForMember(
                dest => dest.UserRoles,
                opt => opt.MapFrom(src => src.UserRoles != null
                    ? src.UserRoles.Select(ur => ur.Role.Name).ToList()
                    : new List<string>()))
            .ReverseMap()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)) // Define UserName como Email
            .ForMember(dest => dest.UserRoles, opt => opt.Ignore()); // Gerenciar UserRoles manualmente

        // User <-> UserLoginDto
        CreateMap<User, UserLoginDto>().ReverseMap();

        // User <-> UserUpdateDto
        CreateMap<User, UserUpdateDto>()
            .ForMember(
                dest => dest.UserRoles,
                opt => opt.MapFrom(src => src.UserRoles != null
                    ? src.UserRoles.Select(ur => ur.Role.Name).ToList()
                    : new List<string>()))
            .ReverseMap()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)) // Define UserName como Email
            .ForMember(dest => dest.UserRoles, opt => opt.Ignore()); // Gerenciar UserRoles manualmente

        // Veiculo
        CreateMap<Veiculo, VeiculoDto>().ReverseMap();

        // Reboque
        CreateMap<ReboqueSolicitacaoDto, ReboqueSolicitacao>().ReverseMap();
        CreateMap<ReboqueCalculoDto, CalculoReboque>().ReverseMap(); // Para persistência no banco


    }
}