namespace FunTrophy.API.Model
{
    public class TimeAdjustement
    {
        public int Id { get; set; }
        public TimeSpan Time { get; set; }
        public int CategoryId { get; set; }
        public int TeamId { get; set; }

        public TimeAdjustementCategory Category { get; set; }
        public Team Team { get; set; }
    }
}