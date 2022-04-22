using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Infrastructure.Repositories
{
    public class TrackRepository : RepositoryBase<Track>, ITrackRepository
    {
        public TrackRepository(FunTrophyContext dbContext) : base(dbContext)
        {
        }

        public Task<List<Track>> GetAll(int raceId)
        {
            return GetAll(x => x.RaceId == raceId);
        }
    }
}