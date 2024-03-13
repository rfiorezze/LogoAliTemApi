using LogoAliTem.Application.Dtos;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace LogoAliTem.Application;

public interface IAccountService
{
    Task<bool> UserExists(string username);
    Task<UserUpdateDto> GetUserByUsernameAsync(string username);
    Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password);
    Task<UserUpdateDto> CreateAccountAsync(UserDto userDto);
    Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto);
}