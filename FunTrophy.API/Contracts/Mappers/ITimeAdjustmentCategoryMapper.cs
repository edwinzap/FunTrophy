using FunTrophy.Infrastructure.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Contracts.Mappers
{
    public interface ITimeAdjustmentCategoryMapper
    {
        TimeAdjustmentCategory Map(AddTimeAdjustmentCategoryDto team);

        TimeAdjustmentCategoryDto Map(TimeAdjustmentCategory team);

        List<TimeAdjustmentCategoryDto> Map(List<TimeAdjustmentCategory> teams);
    }
}