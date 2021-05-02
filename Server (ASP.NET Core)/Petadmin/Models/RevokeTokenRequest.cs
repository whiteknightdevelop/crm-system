namespace Petadmin.Models
{
    public class RevokeTokenRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
