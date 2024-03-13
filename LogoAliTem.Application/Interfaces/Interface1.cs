using LogoAliTem.Application.Dtos;
using System.Threading.Tasks;

namespace LogoAliTem.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(UserUpdateDto userUpdateDto);
    }
}
