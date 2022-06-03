using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Infrastructure.Contracts.Repositories
{
    public interface ITimeAdjustmentRepository : IRepositoryBase<TimeAdjustment>
    {
        Task<List<TimeAdjustment>> GetOfTeam(int teamId);

        Task<List<TimeAdjustment>> GetOfRace(int raceId);
    }
}