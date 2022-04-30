using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Infrastructure.Repositories
{
    public class TimeAdjustmentRepository : RepositoryBase<TimeAdjustment>, ITimeAdjustmentRepository
    {
        public TimeAdjustmentRepository(FunTrophyContext dbContext) : base(dbContext)
        {
        }

        public Task<List<TimeAdjustment>> GetAllOfTeam(int teamId)
        {
            return GetAll(x => x.TeamId == teamId);
        }
    }
}