using LogoAliTem.API.Extensions;
using LogoAliTem.Application;
using LogoAliTem.Application.Dtos;
using LogoAliTem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LogoAliTem.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly ITokenService _tokenService;

    public AccountController(IAccountService accountService, ITokenService tokenService)
    {
        _accountService = accountService;
        _tokenService = tokenService;
    }

    [HttpGet("GetUser")]
    public async Task<IActionResult> GetUser()
    {
        try
        {
            var email = User.GetEmailUser();
            var user = await _accountService.GetUserByEmailAsync(email);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, new
            {
                codigoErro = 500,
                mensagem = ex.Message
            });
        }
    }

    [HttpPost("Register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(UserDto userDto)
    {
        try
        {
            if (await _accountService.UserExists(userDto.Email))
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, new
                {
                    codigoErro = 400,
                    mensagem = "Usuário já cadastrado!"
                });
            }

            var user = await _accountService.CreateAccountAsync(userDto);
            if (user != null)
            {
                // Recupera as roles associadas ao usuário para resposta
                var roles = await _accountService.GetUserRolesAsync(user.Email);

                return Ok(new
                {
                    Email = user.Email,
                    NomeCompleto = user.NomeCompleto,
                    Telefone = user.Telefone,
                    Cpf = user.Cpf,
                    Sexo = user.Sexo,
                    DataNascimento = user.DataNascimento,
                    UserRoles = roles,
                    token = await _tokenService.CreateToken(user)
                });
            }

            return this.StatusCode(StatusCodes.Status400BadRequest, new
            {
                codigoErro = 400,
                mensagem = "Usuário não criado, tente novamente mais tarde!"
            });
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, new
            {
                codigoErro = 500,
                mensagem = ex.Message
            });
        }
    }

    [HttpPut("UpdateUser")]
    public async Task<IActionResult> UpdateUser(UserUpdateDto userUpdateDto)
    {
        try
        {
            if (userUpdateDto.Email != User.GetEmailUser())
                return Unauthorized("Usuário Inválido");

            var user = await _accountService.GetUserByEmailAsync(User.GetEmailUser());
            if (user == null) return Unauthorized("Usuário Inválido");

            var userReturn = await _accountService.UpdateAccount(userUpdateDto);
            if (userReturn == null) return NoContent();

            // Atualizando os roles
            if (userUpdateDto.UserRoles != null && userUpdateDto.UserRoles.Any())
            {
                var rolesResult = await _accountService.UpdateUserRolesAsync(userReturn.Email, userUpdateDto.UserRoles);
                if (!rolesResult)
                {
                    return this.StatusCode(StatusCodes.Status400BadRequest, new
                    {
                        codigoErro = 400,
                        mensagem = "Erro ao atualizar os roles do usuário!"
                    });
                }
            }

            var roles = await _accountService.GetUserRolesAsync(userReturn.Email);

            return Ok(new
            {
                Email = userReturn.Email,
                NomeCompleto = userReturn.NomeCompleto,
                Telefone = userReturn.Telefone,
                Cpf = user.Cpf,
                Sexo = user.Sexo,
                DataNascimento = user.DataNascimento,
                UserRoles = roles,
                token = await _tokenService.CreateToken(userReturn)
            });
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, new
            {
                codigoErro = 500,
                mensagem = ex.Message
            });
        }
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(UserLoginDto userLogin)
    {
        try
        {
            var user = await _accountService.GetUserByEmailAsync(userLogin.Email);
            if (user is null) return Unauthorized("Usuário ou senha inválidos");

            var result = await _accountService.CheckUserPasswordAsync(user, userLogin.Password);
            if (!result.Succeeded) return Unauthorized();

            var roles = await _accountService.GetUserRolesAsync(user.Email);

            return Ok(new
            {
                Email = user.Email,
                NomeCompleto = user.NomeCompleto,
                Telefone = user.Telefone,
                Cpf = user.Cpf,
                Sexo = user.Sexo,
                DataNascimento = user.DataNascimento,
                UserRoles = roles, // Inclui os roles no retorno
                Token = await _tokenService.CreateToken(user)
            });
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, new
            {
                codigoErro = 500,
                mensagem = ex.Message
            });
        }
    }

    [AllowAnonymous]
    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
    {
        try
        {
            var result = await _accountService.SendPasswordResetLinkAsync(forgotPasswordDto.Email);
            if (!result) return BadRequest("Falha ao enviar o e-mail de recuperação de senha.");

            return Ok();
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao processar sua solicitação: {ex.Message}");
        }
    }

    [AllowAnonymous]
    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
    {
        try
        {
            var result = await _accountService.ResetUserPasswordAsync(resetPasswordDto.Email, resetPasswordDto.Token, resetPasswordDto.NewPassword);
            if (!result) return BadRequest("Erro ao redefinir a senha.");

            return Ok();
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao processar sua solicitação: {ex.Message}");
        }
    }
}
