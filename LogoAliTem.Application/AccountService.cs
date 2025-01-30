using AutoMapper;
using LogoAliTem.Application.Dtos;
using LogoAliTem.Application.Interfaces;
using LogoAliTem.Domain.Identity;
using LogoAliTem.Persistence.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogoAliTem.Application;
public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly IMotoristaService _motoristaService;
    private readonly IConfiguration _configuration;

    public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, IUserRepository userRepository, IEmailService emailService, IMotoristaService motoristaService, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _userRepository = userRepository;
        _emailService = emailService;        
        _motoristaService = motoristaService;
        _configuration = configuration;
    }

    public async Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password)
    {
        try
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == userUpdateDto.Email.ToLower());

            return await _signInManager.CheckPasswordSignInAsync(user, password, false);

        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao tentar verificar password. Erro: {ex.Message}");
        }
    }

    public async Task<UserUpdateDto> CreateAccountAsync(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);

        // Certifique-se de que UserName recebe o valor do Email
        user.UserName = user.Email;

        var result = await _userManager.CreateAsync(user, userDto.Password);

        if (result.Succeeded)
        {
            // Atribui os papéis enviados pelo cliente
            if (userDto.UserRoles != null && userDto.UserRoles.Any())
            {
                foreach (var role in userDto.UserRoles)
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
            }

            var motorista = new MotoristaDto
            {
                Nome = user.NomeCompleto,
                Cpf = user.Cpf,
                DataNascimento = user.DataNascimento.ToString(),
                Celular = user.Telefone,
                Email = user.Email,
                Sexo = user.Sexo,                
                PlacaVeiculoPrincipal = userDto.PlacaVeiculoPrincipal
            };

            await _motoristaService.AddMotorista(motorista, user.Id);
            

            return _mapper.Map<UserUpdateDto>(user);
        }

        throw new Exception("Erro ao criar usuário.");
    }

    public async Task<UserUpdateDto> GetUserByEmailAsync(string email)
    {
        try
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user is null) return null;

            var roles = await _userManager.GetRolesAsync(user);

            var userUpdateDto = _mapper.Map<UserUpdateDto>(user);

            userUpdateDto.UserRoles = roles;

            return userUpdateDto;
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao tentar buscar email. Erro: {ex.Message}");
        }
    }

    public async Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto)
    {
        try
        {
            var user = await _userRepository.GetUserByEmailAsync(userUpdateDto.Email);
            if (user is null) return null;

            userUpdateDto.Id = user.Id;

            // Atualiza UserName ao atualizar o Email
            userUpdateDto.Email = userUpdateDto.Email.ToLower();
            user.UserName = userUpdateDto.Email;

            _mapper.Map(userUpdateDto, user);

            _userRepository.Update<User>(user);

            if (await _userRepository.SaveChangesAsync())
            {
                var userRetorno = await _userRepository.GetUserByEmailAsync(user.Email);

                return _mapper.Map<UserUpdateDto>(userRetorno);
            }

            return null;
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao tentar atualizar usuário. Erro: {ex.Message}");
        }
    }

    public async Task<bool> UserExists(string email)
    {
        try
        {
            return await _userManager.Users.AnyAsync(u => u.Email.Equals(email.ToLower()));
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao tentar verificar se usuario existe. Erro: {ex.Message}");
        }
    }

    // Envio de e-mail com link de redefinição de senha
    public async Task<bool> SendPasswordResetLinkAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return false;

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        // Recupera a URL base do appsettings
        var baseUrl = _configuration["Application:BaseUrl"];
        var resetLink = $"{baseUrl}/user/reset-password?token={encodedToken}&email={user.Email}";

        await _emailService.EnviarEmailAsync(email, "Redefinição de Senha", $"Clique no link para redefinir sua senha: {resetLink}");

        return true;
    }

    // Reset de senha com token
    public async Task<bool> ResetUserPasswordAsync(string email, string token, string newPassword)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return false;

        // Decodificando o token recebido pela URL
        var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

        var result = await _userManager.ResetPasswordAsync(user, decodedToken, newPassword);
        return result.Succeeded;
    }

    public async Task<IEnumerable<string>> GetUserRolesAsync(string email)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);
            return roles;
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao obter os roles do usuário. Erro: {ex.Message}");
        }
    }

    public async Task<bool> AddUserRolesAsync(string email, IEnumerable<string> roles)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

            var result = await _userManager.AddToRolesAsync(user, roles);
            return result.Succeeded;
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao adicionar roles ao usuário. Erro: {ex.Message}");
        }
    }


    public async Task<bool> UpdateUserRolesAsync(string email, IEnumerable<string> newRoles)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

            var currentRoles = await _userManager.GetRolesAsync(user);

            // Removendo roles que não estão na nova lista
            var rolesToRemove = currentRoles.Except(newRoles);
            var removeResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

            if (!removeResult.Succeeded) return false;

            // Adicionando novos roles
            var rolesToAdd = newRoles.Except(currentRoles);
            var addResult = await _userManager.AddToRolesAsync(user, rolesToAdd);

            return addResult.Succeeded;
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao atualizar roles do usuário. Erro: {ex.Message}");
        }
    }

}