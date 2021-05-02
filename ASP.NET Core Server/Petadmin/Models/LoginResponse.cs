using Petadmin.Core.Models;
using Petadmin.Identity.Jwt;

namespace Petadmin.Models
{
    public class LoginResponse
    {
        public AccessToken AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
        public ApplicationUserMysqlResponse User { get; set; }
    }
}
