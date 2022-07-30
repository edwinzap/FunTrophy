using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;

namespace FunTrophy.Infrastructure.Repositories
{
    public class TimeAdjustmentRepository : RepositoryBase<TimeAdjustment>, ITimeAdjustmentRepository
    {
        public TimeAdjustmentRepository(FunTrophyContext dbContext) : base(dbContext)
        {
            Includes = new string[] { "Category", "Team" };
        }

        public Task<TimeAdjustment?> GetByTeamAndCategory(int teamId, int categoryId)
        {
            return _dbContext.TimeAdjustments.FirstOrDefaultAsync(x => x.TeamId == teamId && x.CategoryId == categoryId);
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

        public async Task RemoveAllOfRace(int raceId)
        {
            var adjustments = await GetAll(x => x.Team.Color.RaceId == raceId);
            _dbContext.TimeAdjustments.RemoveRange(adjustments);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAllOfTeam(int teamId)
        {
            var adjustments = await GetAll(x => x.TeamId == teamId);
            _dbContext.TimeAdjustments.RemoveRange(adjustments);
            await _dbContext.SaveChangesAsync();
        }
    }
}