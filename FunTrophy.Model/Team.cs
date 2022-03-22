﻿namespace FunTrophy.Model
{
    public class Team
    {
        public int Id { get; set; }
        public TeamType Type { get; set; }
        public int ColorId { get; set; }
        public int RaceId { get; set; }

        public virtual Race Race { get; set; }
        public virtual Color Color { get; set; }

        public virtual ICollection<TrackTime> TrackTimes { get; set; }
        public virtual ICollection<TimeAdjustement> TimeAdjustements { get; set; }
    }

    public enum TeamType
    {
        Family = 0,
        Warrior = 1,
    }
}