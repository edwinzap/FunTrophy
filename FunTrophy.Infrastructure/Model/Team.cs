﻿namespace FunTrophy.Infrastructure.Model
{
    public class Team : EntityBase
    {
        public string Name { get; set; }
        public int Number { get; set; }

        public TeamType Type { get; set; }
        public int ColorId { get; set; }

        public virtual Color Color { get; set; }

        public List<TrackTime> TrackTimes { get; set; }
        public List<TimeAdjustment> TimeAdjustements { get; set; }
    }

    public enum TeamType
    {
        Family = 0,
        Warrior = 1,
    }
}