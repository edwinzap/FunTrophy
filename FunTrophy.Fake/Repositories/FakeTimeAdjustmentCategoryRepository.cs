using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Fake.Repositories
{
    public class FakeTimeAdjustmentCategoryRepository : FakeRepositoryBase<TimeAdjustmentCategory>, ITimeAdjustmentCategoryRepository
    {
        public FakeTimeAdjustmentCategoryRepository(FakeDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<TimeAdjustmentCategory>> GetOfRace(int raceId)
        {
            return GetAll(x => x.RaceId == raceId);
        }
    }
}