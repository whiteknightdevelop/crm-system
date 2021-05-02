using System;

namespace Petadmin.Identity.Jwt
{
    public class AccessToken
    {
        /// <summary>
        /// Gets or sets the token string.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the token valid to.
        /// </summary>
        public DateTime Expires { get; set; }
    }
}
