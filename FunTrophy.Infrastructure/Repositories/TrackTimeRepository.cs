using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Infrastructure.Repositories
{
    public class TrackTimeRepository : RepositoryBase<TrackTime>, ITrackTimeRepository
    {
        public TrackTimeRepository(FunTrophyContext dbContext) : base(dbContext)
        {
            Includes = new string[] { "Track", "Team", "Team.Color" };
        }

        public Task<List<TrackTime>> GetOfColor(int colorId)
        {
            return GetAll(x => x.Team.ColorId == colorId);
        }

        public Task<List<TrackTime>> GetOfRace(int raceId)
        {
            return GetAll(x => x.Track.RaceId == raceId);
        }

        public Task<List<TrackTime>> GetOfTeam(int teamId)
        {
            return GetAll(x => x.TeamId == teamId);
        }

        public Task<List<TrackTime>> GetOfTrack(int trackId)
        {
            return GetAll(x => x.TrackId == trackId);
        }

        public async Task RemoveAllOfRace(int raceId)
        {
            var times = await GetAll(x => x.Track.RaceId == raceId);
            _dbContext.TrackTimes.RemoveRange(times);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAllOfTeam(int teamId)
        {
            var times = await GetAll(x => x.TeamId == teamId);
            _dbContext.TrackTimes.RemoveRange(times);
            await _dbContext.SaveChangesAsync();
        }
    }
}