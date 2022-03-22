namespace FunTrophy.Model
{
    public class TrackTime
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TrackId { get; set; }
        public int TeamId { get; set; }

        public virtual Track Track { get; set; }
        public virtual Team Team { get; set; }
    }
}