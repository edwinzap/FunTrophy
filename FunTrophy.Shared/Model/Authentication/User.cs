using System.ComponentModel.DataAnnotations;

namespace FunTrophy.Shared.Model.Authentication
{
    public class User
    {
        [Required(ErrorMessage = "Email Address is required.")]
        public string Pseudo { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
