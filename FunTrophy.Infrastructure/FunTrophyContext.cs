using FunTrophy.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;

namespace FunTrophy.Infrastructure
{
    public class FunTrophyContext : DbContext
    {
        public FunTrophyContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Race> Races { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<TrackOrder> TrackOrders { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TrackTime> TrackTimes { get; set; }
        public DbSet<TimeAdjustment> TimeAdjustments { get; set; }
        public DbSet<TimeAdjustmentCategory> TimeAdjustmentCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.NoAction;
            }
        }
    }
}