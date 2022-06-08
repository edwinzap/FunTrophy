using System.ComponentModel.DataAnnotations;

namespace FunTrophy.Shared.Model
{
    public class AddUserDto
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}