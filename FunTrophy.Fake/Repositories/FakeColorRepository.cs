using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Fake.Repositories
{
    public class FakeColorRepository : FakeRepositoryBase<Color>, IColorRepository
    {
        public FakeColorRepository(FakeDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<Color>> GetAll(int raceId)
        {
            return GetAll(x => x.RaceId == raceId);
        }
    }
}