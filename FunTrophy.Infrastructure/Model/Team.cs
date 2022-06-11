namespace FunTrophy.Infrastructure.Model
{
    public class Team : EntityBase
    {
        public string Name { get; set; }
        public int Number { get; set; }

        public TeamType Type { get; set; }
        public int ColorId { get; set; }

        public virtual Color Color { get; set; }

        public List<TrackTime> TrackTimes { get; set; }
        public List<TimeAdjustment> TimeAdjustments { get; set; }
    }

    public enum TeamType
    {
        Fun = 0,
        Warrior = 1,
    }
}