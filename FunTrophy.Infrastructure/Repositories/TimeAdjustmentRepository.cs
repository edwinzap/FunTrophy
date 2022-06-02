using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Infrastructure.Repositories
{
    public class TimeAdjustmentRepository : RepositoryBase<TimeAdjustment>, ITimeAdjustmentRepository
    {
        public TimeAdjustmentRepository(FunTrophyContext dbContext) : base(dbContext)
        {
            Includes = new string[] { "Category" };
        }

        public Task<List<TimeAdjustment>> GetOfRace(int raceId)
        {
            return GetAll(x => x.Team.Color.RaceId == raceId);
        }

        public Task<List<TimeAdjustment>> GetOfTeam(int teamId)
        {
            return GetAll(x => x.TeamId == teamId);
        }
    }
}