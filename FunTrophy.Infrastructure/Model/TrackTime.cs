namespace FunTrophy.Infrastructure.Model
{
    public class TrackTime : EntityBase
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TrackId { get; set; }
        public int TeamId { get; set; }

        public Track Track { get; set; }
        public Team Team { get; set; }
    }
}