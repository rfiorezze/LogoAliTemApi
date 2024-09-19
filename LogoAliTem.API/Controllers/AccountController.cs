using LogoAliTem.API.Extensions;
using LogoAliTem.Application;
using LogoAliTem.Application.Dtos;
using LogoAliTem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
                return Ok(new
                {
                    Email = user.Email,
                    NomeCompleto = user.NomeCompleto,
                    token = _tokenService.CreateToken(user).Result
                });

            return this.StatusCode(StatusCodes.Status400BadRequest, new
            {
                codigoErro = 400,
                mensagem = "Usuario não criado, tente novamente mais tarde!"
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

            return Ok(new
            {
                Email = userReturn.Email,
                NomeCompleto = userReturn.NomeCompleto,
                token = _tokenService.CreateToken(userReturn).Result
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
            if (user is null) return Unauthorized("Usuario ou Senha inválido");

            var result = await _accountService.CheckUserPasswordAsync(user, userLogin.Password);
            if (!result.Succeeded) return Unauthorized();

            return Ok(new
            {
                Email = user.Email,
                NomeCompleto = user.NomeCompleto,
                token = _tokenService.CreateToken(user).Result
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
}
