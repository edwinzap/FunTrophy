using FunTrophy.Shared.Model;

namespace FunTrophy.API.Contracts.Services
{
    public interface ITimeAdjustmentService
    {
        Task<int> Create(AddTimeAdjustmentDto timeAdjustment);
        Task<List<TimeAdjustmentDto>> GetAll(int teamId);
        Task Remove(int timeAdjustmentId);
        Task Update(int timeAdjustmentId, UpdateTimeAdjustmentDto timeAdjustment);
    }
}