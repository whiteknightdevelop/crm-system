﻿namespace Petadmin.Models
{
    public class RefreshAccessTokenRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string RemoteIpAddress { get; set; }
    }
}
