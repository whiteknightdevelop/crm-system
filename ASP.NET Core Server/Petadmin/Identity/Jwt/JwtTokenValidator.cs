using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Petadmin.Identity.Jwt.Interfaces;

namespace Petadmin.Identity.Jwt
{
    public class JwtTokenValidator : IJwtTokenValidator
    {
        private readonly IJwtTokenHandler _jwtTokenHandler;
        private readonly IConfiguration _configuration;

        public JwtTokenValidator(IJwtTokenHandler jwtTokenHandler, IConfiguration configuration)
        {
            _jwtTokenHandler = jwtTokenHandler;
            _configuration = configuration;
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            return _jwtTokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtSettings:SigningKey"])),
                ValidateLifetime = false 
            });
        }
    }
}
