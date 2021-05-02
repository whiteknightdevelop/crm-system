using System;

namespace Petadmin.Core.Models
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public int UserId { get; set; }
        public bool Active => DateTime.Now <= Expires;
        public string RemoteIpAddress { get; set; }

        public RefreshToken(string token, DateTime expires, int userId, string remoteIpAddress)
        {
            Token = token;
            Expires = expires;
            UserId = userId;
            RemoteIpAddress = remoteIpAddress;
        }
    }
}
