using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Infrastructure.Repositories
{
    public class TimeAdjustmentCategoryRepository : RepositoryBase<TimeAdjustmentCategory>, ITimeAdjustmentCategoryRepository
    {
        public TimeAdjustmentCategoryRepository(FunTrophyContext dbContext) : base(dbContext)
        {
        }

        public Task<List<TimeAdjustmentCategory>> GetOfRace(int raceId)
        {
            return GetAll(x => x.RaceId == raceId);
        }
    }
}