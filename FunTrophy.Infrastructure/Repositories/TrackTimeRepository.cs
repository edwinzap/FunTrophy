using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Infrastructure.Repositories
{
    public class TrackTimeRepository : RepositoryBase<TrackTime>, ITrackTimeRepository
    {
        public TrackTimeRepository(FunTrophyContext dbContext) : base(dbContext)
        {
            Includes = new string[] { "Track" };
        }

        public Task<List<TrackTime>> GetOfColor(int colorId)
        {
            return GetAll(x => x.Team.ColorId == colorId);
        }

        public Task<List<TrackTime>> GetOfTeam(int teamId)
        {
            return GetAll(x => x.TeamId == teamId);
        }
    }
}