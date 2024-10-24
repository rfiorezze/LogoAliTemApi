using LogoAliTem.Application.Dtos;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace LogoAliTem.Application;

public interface IAccountService
{
    Task<bool> UserExists(string email);
    Task<UserUpdateDto> GetUserByEmailAsync(string email);
    Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password);
    Task<UserUpdateDto> CreateAccountAsync(UserDto userDto);
    Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto);
    Task<bool> SendPasswordResetLinkAsync(string email);
    Task<bool> ResetUserPasswordAsync(string email, string token, string newPassword);
}