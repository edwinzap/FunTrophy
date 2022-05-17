using System.ComponentModel.DataAnnotations;

namespace FunTrophy.Shared.Model
{
    public class AddColorDto
    {
        [Required]
        public string Code { get; set; }
        public int RaceId { get; set; }
    }
}