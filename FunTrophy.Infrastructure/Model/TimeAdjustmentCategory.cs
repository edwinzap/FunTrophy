namespace FunTrophy.Infrastructure.Model
{
    public class TimeAdjustmentCategory : EntityBase
    {
        public int RaceId { get; set; }
        public string Name { get; set; }

        public virtual Race Race { get; set; }
    }
}