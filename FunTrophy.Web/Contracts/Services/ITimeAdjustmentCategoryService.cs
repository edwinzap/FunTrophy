using FunTrophy.Shared.Model;

namespace FunTrophy.Web.Contracts.Services
{
    public interface ITimeAdjustmentCategoryService
    {
        Task Add(AddTimeAdjustmentCategoryDto category);

        Task<List<TimeAdjustmentCategoryDto>> GetCategories(int raceId);

        Task Remove(int categoryId);

        Task Update(int categoryId, UpdateTimeAdjustmentCategoryDto category);
    }
}