using FunTrophy.Infrastructure;
using FunTrophy.Infrastructure.Model;
using FunTrophy.Tests.Utils;

namespace FunTrophy.Fake
{
    public class FunTrophyInitializer
    {
        private readonly FunTrophyContext _dbContext;

        public FunTrophyInitializer(FunTrophyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Race> Races { get; private set; } = new();

        public List<Color> Colors { get; private set; } = new();

        public List<Team> Teams { get; } = new();

        public List<Track> Tracks { get; private set; } = new();

        public List<TrackOrder> TrackOrders { get; private set; } = new();

        public List<TimeAdjustmentCategory> TimeAdjustmentCategories { get; private set; } = new();
        public List<TimeAdjustment> TimeAdjustments { get; private set; } = new();

        public List<TrackTime> TrackTimes { get; private set; } = new();

        public List<User> Users { get; private set; } = new();

        #region Seed

        public async Task CreateDatabase()
        {
            await _dbContext.Database.EnsureDeletedAsync();
            await _dbContext.Database.EnsureCreatedAsync();
        }

        public void SeedBasicData()
        {
            SeedUsers();
        }

        public void SeedData()
        {
            SeedRaces();
            SeedColors();
            SeedTeams();
            SeedTracks();
            SeedTrackOrders();
            SeedTimeAdjustmentCategories();
            SeedUsers();
        }

        public async Task ApplySeeding()
        {
            var isDbCreated = await _dbContext.Database.EnsureCreatedAsync();
            if (!isDbCreated)
                await CreateDatabase();
            
            _dbContext.Races.AddRange(Races);
            _dbContext.Colors.AddRange(Colors);
            _dbContext.Teams.AddRange(Teams);
            _dbContext.Tracks.AddRange(Tracks);
            _dbContext.TrackOrders.AddRange(TrackOrders);
            _dbContext.TimeAdjustmentCategories.AddRange(TimeAdjustmentCategories);
            _dbContext.Users.AddRange(Users);

            await _dbContext.SaveChangesAsync();
        }

        private void SeedRaces()
        {
            Races = new List<Race>()
            {
                new Race() { Name = "FunTrophy 2021", Date = new DateTime(2021, 6, 25) },
                new Race() { Name = "FunTrophy 2022", Date = new DateTime(2022, 6, 25) },
            };
        }

        private void SeedColors()
        {
            var race = Races.Last();
            Colors = new List<Color>()
            {
                new Color { Code = "#eb4034", Race = race },
                new Color { Code = "#ebb434", Race = race },
                new Color { Code = "#4feb34", Race = race },
                new Color { Code = "#34e8eb", Race = race },
                new Color { Code = "#3452eb", Race = race },
                new Color { Code = "#c034eb", Race = race },
            };
        }

        private void SeedTeams()
        {
            foreach (var color in Colors)
            {
                for (int i = 1; i <= Some.Int(8, 10); i++)
                {
                    var team = new Team { Name = Some.CompanyName(), Number = i, Type = Some.Generated<TeamType>(), Color = color };
                    Teams.Add(team);
                }
            }
        }

        private void SeedTracks()
        {
            var race = Races.Last();
            for (int index = 1; index < Some.Int(6, 10); index++)
            {
                var track = new Track 
                { 
                    Name = Some.CompanyName(), 
                    Race = race,
                    Number = index,
                };
                Tracks.Add(track);
            }
        }

        private static Random random = new Random();

        private void SeedTrackOrders()
        {
            foreach (var color in Colors)
            {
                var tracks = new List<Track>(Tracks);
                var sortOrder = 1;
                while (tracks.Any())
                {
                    var index = random.Next(tracks.Count);
                    var track = tracks[index];
                    var trackOrder = new TrackOrder
                    {
                        SortOrder = sortOrder,
                        Track = track,
                        Color = color,
                    };
                    TrackOrders.Add(trackOrder);
                    tracks.RemoveAt(index);
                }
            }
        }

        private void SeedTimeAdjustmentCategories()
        {
            var race = Races.Last();
            TimeAdjustmentCategories = new List<TimeAdjustmentCategory>
            {
                new TimeAdjustmentCategory { Name = "Pénalités", Race = race},
                new TimeAdjustmentCategory { Name = "Tir à l'arc", Race = race},
                new TimeAdjustmentCategory { Name = "Frisbee", Race = race},
            };
        }

        private void SeedUsers()
        {
            Users = new List<User>()
            {
                new User { FirstName = "Miguel", LastName = "Forget", IsAdmin = true, Password = "admin", UserName = "admin"},
                new User { FirstName = "Toto", LastName = "Dupont", IsAdmin = false, Password = "user", UserName = "user"},
            };
        }

        #endregion Seed
    }
}