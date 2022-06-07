using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Fake.Repositories
{
    public class FakeTrackTimeRepository : FakeRepositoryBase<TrackTime>, ITrackTimeRepository
    {
        public FakeTrackTimeRepository(FakeDbContext dbContext) : base(dbContext)
        {
        }

        public override Task<int> Add(TrackTime entity)
        {
            var team = _dbContext.Teams.First(x => x.Id == entity.TeamId);
            var track = _dbContext.Tracks.First(x => x.Id == entity.TrackId);
            entity.TrackId = track.Id;
            entity.Track = track;
            entity.TeamId = team.Id;
            entity.Team = team;
            return base.Add(entity);
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

        public Task RemoveAllOfRace(int raceId)
        {
            _dbContext.TrackTimes.RemoveAll(x => x.Track.RaceId == raceId);
            return Task.CompletedTask;
        }
    }
}