using Petadmin.Core.Models;
using Petadmin.Identity.Jwt;

namespace Petadmin.Models
{
    public class RefreshAccessTokenResponse
    {
        public AccessToken AccessToken { get; set; }
        public ApplicationUserMysqlResponse User { get; set; }
    }
}
