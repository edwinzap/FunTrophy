using System.ComponentModel.DataAnnotations.Schema;

namespace FunTrophy.Infrastructure.Model
{
    public class TrackTime : EntityBase
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int TrackId { get; set; }
        public int TeamId { get; set; }
        public int ModifyByUserId { get; set; }

        public Track Track { get; set; }
        public Team Team { get; set; }

        [ForeignKey("ModifyByUserId")]
        public User User { get; set; }
    }
}