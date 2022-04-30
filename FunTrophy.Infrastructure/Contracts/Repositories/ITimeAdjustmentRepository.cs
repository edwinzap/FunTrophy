using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Infrastructure.Contracts.Repositories
{
    public interface ITimeAdjustmentRepository : IRepositoryBase<TimeAdjustment>
    {
        Task<List<TimeAdjustment>> GetAllOfTeam(int teamId);
    }
}