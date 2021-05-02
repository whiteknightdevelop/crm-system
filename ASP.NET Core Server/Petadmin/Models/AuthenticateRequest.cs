using System.ComponentModel.DataAnnotations;

namespace Petadmin.Models
{
    public class AuthenticateRequest
    {
        [Required (ErrorMessage = "Username is required!")]
        public string UserName { get; set; }
        [Required (ErrorMessage = "Password is required!")]
        public string Password { get; set; }
        public string Gender { get; set; }
    }
}
