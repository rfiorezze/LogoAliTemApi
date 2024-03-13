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
            var userName = User.GetUserName();
            var user = await _accountService.GetUserByUsernameAsync(userName);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar buscar usuário. Erro: {ex.Message}");
        }
    }

    [HttpPost("Register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(UserDto userDto)
    {
        try
        {
            if (await _accountService.UserExists(userDto.Username))
                return BadRequest("Usuario já existe");

            var user = await _accountService.CreateAccountAsync(userDto);
            if (user != null)
                return Ok(new
                {
                    userName = user.UserName,
                    NomeCompleto = user.NomeCompleto,
                    token = _tokenService.CreateToken(user).Result
                });

            return BadRequest("Usuario não criado, tente novamente mais tarde!");
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar criar usuário. Erro: {ex.Message}");
        }
    }

    [HttpPut("UpdateUser")]
    public async Task<IActionResult> UpdateUser(UserUpdateDto userUpdateDto)
    {
        try
        {
            if (userUpdateDto.UserName != User.GetUserName())
                return Unauthorized("Usuário Inválido");

            var user = await _accountService.GetUserByUsernameAsync(User.GetUserName());
            if (user == null) return Unauthorized("Usuário Inválido");

            var userReturn = await _accountService.UpdateAccount(userUpdateDto);
            if (userReturn == null) return NoContent();

            return Ok(new
            {
                UserName = userReturn.UserName,
                NomeCompleto = userReturn.NomeCompleto,
                token = _tokenService.CreateToken(userReturn).Result
            });
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar Atualizar Usuário. Erro: {ex.Message}");
        }
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(UserLoginDto userLogin)
    {
        try
        {
            var user = await _accountService.GetUserByUsernameAsync(userLogin.Username);
            if (user is null) return Unauthorized("Usuario ou Senha inválido");

            var result = await _accountService.CheckUserPasswordAsync(user, userLogin.Password);
            if (!result.Succeeded) return Unauthorized();

            return Ok(new
            {
                userName = user.UserName,
                NomeCompleto = user.NomeCompleto,
                token = _tokenService.CreateToken(user).Result
            });
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar criar usuário. Erro: {ex.Message}");
        }
    }
}
