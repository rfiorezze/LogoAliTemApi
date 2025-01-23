using AutoMapper;
using LogoAliTem.Application.Dtos;
using LogoAliTem.Domain;
using LogoAliTem.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogoAliTem.Application.Helpers
{
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
                .ForMember(
                    dest => dest.UserRoles,
                    opt => opt.Ignore()); // Atribuir UserRoles manualmente no backend

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
                .ForMember(
                    dest => dest.UserRoles,
                    opt => opt.Ignore()); // Gerenciar manualmente no backend

            // Veiculo
            CreateMap<Veiculo, VeiculoDto>().ReverseMap();
        }
    }
}