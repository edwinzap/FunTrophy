namespace FunTrophy.Infrastructure.Model
{
    public class TimeAdjustment : EntityBase
    {
        public int Seconds { get; set; }
        public int CategoryId { get; set; }
        public int TeamId { get; set; }

        public TimeAdjustmentCategory Category { get; set; }
        public Team Team { get; set; }
    }
}