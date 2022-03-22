using System.Data.Entity;

namespace FunTrophy.Model
{
    public class FunTrophyContext : DbContext
    {
        public virtual DbSet<Race> Races { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }
        public virtual DbSet<TrackOrder> TrackOrders { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TrackTime> TrackTimes { get; set; }
        public virtual DbSet<TimeAdjustement> TimeAdjustements { get; set; }
    }
}