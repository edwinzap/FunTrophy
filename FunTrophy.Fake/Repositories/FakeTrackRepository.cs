using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Fake.Repositories
{
    public class FakeTrackRepository : FakeRepositoryBase<Track>, ITrackRepository
    {
        public FakeTrackRepository(FakeDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<Track>> GetAll(int raceId)
        {
            return GetAll(x => x.RaceId == raceId);
        }
    }
}