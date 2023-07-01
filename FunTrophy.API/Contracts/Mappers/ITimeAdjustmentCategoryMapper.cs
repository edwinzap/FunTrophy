using FunTrophy.Infrastructure.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Contracts.Mappers
{
    public interface ITimeAdjustmentCategoryMapper
    {
        TimeAdjustmentCategory Map(AddTimeAdjustmentCategoryDto timeAdjustmentCategory);

        TimeAdjustmentCategoryDto Map(TimeAdjustmentCategory timeAdjustmentCategory);

        List<TimeAdjustmentCategoryDto> Map(List<TimeAdjustmentCategory> timeAdjustmentCategories);
    }
}