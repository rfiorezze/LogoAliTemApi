using AutoMapper;
using LogoAliTem.Application.Dtos;
using LogoAliTem.Domain.Identity;
using LogoAliTem.Persistence.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LogoAliTem.Application
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, IUserRepository userRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _userRepository = userRepository;
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
            try
            {
                var user = _mapper.Map<User>(userDto);

                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (result.Succeeded)
                {
                    return _mapper.Map<UserUpdateDto>(user);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar criar usuario. Erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = await _userRepository.GetUserByEmailAsync(email);
                if (user is null) return null;

                var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
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

                _mapper.Map(userUpdateDto, user);

                if (userUpdateDto.Password != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    await _userManager.ResetPasswordAsync(user, token, userUpdateDto.Password);
                }

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
                throw new Exception($"Erro ao tentar atualizar usuario. Erro: {ex.Message}");
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
    }
}