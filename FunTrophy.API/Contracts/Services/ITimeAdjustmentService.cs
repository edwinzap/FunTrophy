using FunTrophy.Shared.Model;

namespace FunTrophy.API.Contracts.Services
{
    public interface ITimeAdjustmentService
    {
        Task<int> Create(AddTimeAdjustmentDto timeAdjustment);
        Task<List<TimeAdjustmentDto>> GetAllOfTeam(int teamId);
        Task Remove(int timeAdjustmentId);
        Task<List<TeamTimeAdjustmentDto>> GetAllOfCategory(int categoryId);
        Task<int?> Update(AddTimeAdjustmentDto timeAdjustement);
    }
}