using FunTrophy.API.Contracts.Mappers;
using FunTrophy.Infrastructure.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Mappers
{
    public class TimeAdjustmentCategoryMapper : ITimeAdjustmentCategoryMapper
    {
        public TimeAdjustmentCategoryMapper()
        {
        }

        public TimeAdjustmentCategory Map(AddTimeAdjustmentCategoryDto timeAdjustmentCategory)
        {
            return new TimeAdjustmentCategory
            {
                Name = timeAdjustmentCategory.Name,
                RaceId = timeAdjustmentCategory.RaceId,
            };
        }

        public TimeAdjustmentCategoryDto Map(TimeAdjustmentCategory timeAdjustmentCategory)
        {
            return new TimeAdjustmentCategoryDto
            {
                Id = timeAdjustmentCategory.Id,
                Name = timeAdjustmentCategory.Name,
                Description = timeAdjustmentCategory.Description
            };
        }

        public List<TimeAdjustmentCategoryDto> Map(List<TimeAdjustmentCategory> timeAdjustmentCategorys)
        {
            return timeAdjustmentCategorys.Select(x => Map(x)).ToList();
        }
    }
}