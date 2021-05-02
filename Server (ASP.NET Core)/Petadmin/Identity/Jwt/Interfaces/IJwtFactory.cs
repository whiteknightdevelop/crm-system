using System.Collections.Generic;

namespace Petadmin.Identity.Jwt.Interfaces
{
    public interface IJwtFactory
    {
        AccessToken GenerateEncodedToken(IList<string> userRoles, string userName);
    }
}
