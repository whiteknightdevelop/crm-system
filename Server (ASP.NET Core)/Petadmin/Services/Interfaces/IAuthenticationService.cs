using System.Threading.Tasks;
using Petadmin.Models;

namespace Petadmin.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<RegisterResponse> RegisterAsync(AuthenticateRequest model);
        Task<LoginResponse> LoginAsync(string userName, string password, string remoteIpAddress);
        Task<RefreshAccessTokenResponse> RefreshAccessTokenAsync(RefreshAccessTokenRequest request);
        Task<bool> RevokeTokenAsync(RevokeTokenRequest request);
    }
}
