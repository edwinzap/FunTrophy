using FunTrophy.Shared.Model;

namespace FunTrophy.API.Contracts.Services
{
    public interface ITimeAdjustmentCategoryService
    {
        Task<int> Create(AddTimeAdjustmentCategoryDto category);
        Task<List<TimeAdjustmentCategoryDto>> GetAll(int raceId);
        Task Remove(int categoryId);
        Task Update(int categoryId, UpdateTimeAdjustmentCategoryDto category);
    }
}