using FunTrophy.Infrastructure.Model;
using FunTrophy.Tests.Utils;

namespace FunTrophy.Fake
{
    public class FakeDbContext
    {
        public FakeDbContext()
        {
            SeedData();
        }

        public List<T> Get<T>() where T: EntityBase
        {
            var property = this.GetType().GetProperties()
                .First(x => x.PropertyType == typeof(List<T>))
                .GetValue(this);
            if (property == null)
                throw new KeyNotFoundException();
            
            return (List<T>)property;
        }

        public List<Race> Races { get; private set; } = new();

        public List<Color> Colors { get; private set; } = new();

        public List<Team> Teams { get; } = new();

        public List<Track> Tracks { get; private set; } = new();

        public List<TrackOrder> TrackOrders { get; private set; } = new();

        public List<TimeAdjustmentCategory> TimeAdjustmentCategories { get; private set; } = new();
        public List<TimeAdjustment> TimeAdjustments { get; private set; } = new();

        #region Seed

        private void SeedData()
        {
            SeedRaces();
            SeedColors();
            SeedTeams();
            SeedTracks();
            SeedTimeAdjustmentCategories();
        }

        private void SeedRaces()
        {
            Races = new List<Race>()
            {
                new Race() { Id = 1, Name = "FunTrophy 2020", Date = new DateTime(2020, 6, 25)},
                new Race() { Id = 2, Name = "FunTrophy 2021", Date = new DateTime(2021, 6, 25)},
                new Race() { Id = 3, Name = "FunTrophy 2022", Date = new DateTime(2022, 6, 25)},
            };
        }

        private void SeedColors()
        {
            var race = Races.Last();
            Colors = new List<Color>()
            {
                new Color { Id = 1, Code = "#eb4034", Race = race, RaceId = race.Id },
                new Color { Id = 2, Code = "#ebb434", Race = race, RaceId = race.Id},
                new Color { Id = 3, Code = "#4feb34", Race = race, RaceId = race.Id},
                new Color { Id = 4, Code = "#34e8eb", Race = race, RaceId = race.Id},
                new Color { Id = 5, Code = "#3452eb", Race = race, RaceId = race.Id},
                new Color { Id = 6, Code = "#c034eb", Race = race, RaceId = race.Id},
            };
        }

        private void SeedTeams()
        {
            var index = 1;
            foreach (var color in Colors)
            {
                for (int i = 1; i <= Some.Int(8, 10); i++)
                {
                    var team = new Team { Id = index++, Name = Some.CompanyName(), Number = i, Type = Some.Generated<TeamType>(), Color = color, ColorId = color.Id };
                    Teams.Add(team);
                }
            }
        }

        private void SeedTracks()
        {
            var race = Races.Last();
            for (int index = 1; index < Some.Int(6,10); index++)
            {
                var track = new Track { Id = index, Name = Some.CompanyName(), Race = race, RaceId = race.Id };
                Tracks.Add(track);
            }
        }

        private void SeedTimeAdjustmentCategories()
        {
            var race = Races.Last();
            TimeAdjustmentCategories = new List<TimeAdjustmentCategory>
            {
                new TimeAdjustmentCategory { Id = 1, Name = "Pénalités", Race = race, RaceId = race.Id},
                new TimeAdjustmentCategory { Id = 2, Name = "Tir à l'arc", Race = race, RaceId = race.Id},
                new TimeAdjustmentCategory { Id = 3, Name = "Frisbee", Race = race, RaceId = race.Id},
            };
        }

        #endregion Seed
    }
}