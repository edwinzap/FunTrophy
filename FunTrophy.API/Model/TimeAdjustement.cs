namespace FunTrophy.API.Model
{
    public class TimeAdjustement
    {
        public int Id { get; set; }
        public TimeSpan Time { get; set; }
        public int? TrackId { get; set; }
        public int TeamId { get; set; }

        public Track? Track { get; set; }
        public Team Team { get; set; }
    }
}