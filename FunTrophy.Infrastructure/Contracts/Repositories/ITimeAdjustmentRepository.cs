using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Infrastructure.Contracts.Repositories
{
    public interface ITimeAdjustmentRepository : IRepositoryBase<TimeAdjustment>
    {
        Task<List<TimeAdjustment>> GetOfTeam(int teamId);

        Task<List<TimeAdjustment>> GetOfRace(int raceId);

        Task<List<TimeAdjustment>> GetOfCategory(int categoryId);

        Task RemoveAllOfRace(int raceId);

        Task RemoveAllOfTeam(int teamId);

        Task<TimeAdjustment?> GetByTeamAndCategory(int teamId, int categoryId);
    }
}