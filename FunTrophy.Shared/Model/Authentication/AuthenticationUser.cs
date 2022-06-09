using System.ComponentModel.DataAnnotations;

namespace FunTrophy.Shared.Model.Authentication
{
    public class AuthenticationUser
    {
        [Required(ErrorMessage = "Email Address is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
