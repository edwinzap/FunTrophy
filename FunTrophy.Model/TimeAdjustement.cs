﻿namespace FunTrophy.Model
{
    public class TimeAdjustement
    {
        public int Id { get; set; }
        public TimeSpan Time { get; set; }
        public int? TrackId { get; set; }
        public int TeamId { get; set; }

        public virtual Track? Track { get; set; }
        public virtual Team Team { get; set; }
    }
}