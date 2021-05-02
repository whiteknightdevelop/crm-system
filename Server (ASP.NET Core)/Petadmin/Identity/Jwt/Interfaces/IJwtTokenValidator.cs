using System.Security.Claims;


namespace Petadmin.Identity.Jwt.Interfaces
{
    public interface IJwtTokenValidator
    {
        ClaimsPrincipal GetPrincipalFromToken(string token);
    }
}
