using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Fake.Repositories
{
    public class FakeTimeAdjustmentRepository : FakeRepositoryBase<TimeAdjustment>, ITimeAdjustmentRepository
    {
        public FakeTimeAdjustmentRepository(FakeDbContext dbContext) : base(dbContext)
        {
        }

        public override Task<int> Add(TimeAdjustment entity)
        {
            var category = _dbContext.TimeAdjustmentCategories.First(x => x.Id == entity.CategoryId);
            entity.Category = category;
            return base.Add(entity);
        }

        public Task<TimeAdjustment?> GetByTeamAndCategory(int teamId, int categoryId)
        {
            var timeAdjustment = _dbContext.TimeAdjustments.FirstOrDefault(x => x.TeamId == teamId && x.CategoryId == categoryId);
            return Task.FromResult(timeAdjustment);
        }

        public Task<List<TimeAdjustment>> GetOfCategory(int categoryId)
        {
            return GetAll(x => x.CategoryId == categoryId);
        }

        public Task<List<TimeAdjustment>> GetOfRace(int raceId)
        {
            return GetAll(x => x.Team.Color.RaceId == raceId);
        }

        public Task<List<TimeAdjustment>> GetOfTeam(int teamId)
        {
            return GetAll(x => x.TeamId == teamId);
        }

        public Task RemoveAllOfRace(int raceId)
        {
            _dbContext.TimeAdjustments.RemoveAll(x => x.Team.Color.RaceId == raceId);
            return Task.CompletedTask;
        }
    }
}