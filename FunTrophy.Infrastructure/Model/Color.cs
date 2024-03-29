﻿namespace FunTrophy.Infrastructure.Model
{
    public class Color : EntityBase
    {
        public int RaceId { get; set; }
        public string Code { get; set; }

        public virtual Race Race { get; set; }
        public List<Team> Teams { get; set; }
        public List<TrackOrder> TrackOrders { get; set; }
    }
}