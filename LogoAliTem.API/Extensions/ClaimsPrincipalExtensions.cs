using System.Security.Claims;

namespace LogoAliTem.API.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetEmailUser(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.Email)?.Value;
    }

    public static int GetUserId(this ClaimsPrincipal user)
    {
        return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
}
