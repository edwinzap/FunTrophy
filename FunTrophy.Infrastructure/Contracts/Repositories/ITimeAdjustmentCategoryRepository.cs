using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Infrastructure.Contracts.Repositories
{
    public interface ITimeAdjustmentCategoryRepository : IRepositoryBase<TimeAdjustmentCategory>
    {
        Task<List<TimeAdjustmentCategory>> GetOfRace(int raceId);
    }
}