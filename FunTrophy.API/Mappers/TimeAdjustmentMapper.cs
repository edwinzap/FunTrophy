using FunTrophy.Infrastructure.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Mappers
{
    public class TimeAdjustmentMapper : ITimeAdjustmentMapper
    {
        public TimeAdjustmentMapper()
        {
        }

        public TimeAdjustment Map(AddTimeAdjustmentDto timeAdjustment)
        {
            return new TimeAdjustment
            {
                TeamId = timeAdjustment.TeamId,
                CategoryId = timeAdjustment.CategoryId,
                Time = timeAdjustment.Time,
            };
        }

        public TimeAdjustmentDto Map(TimeAdjustment timeAdjustment)
        {
            return new TimeAdjustmentDto
            {
                Id = timeAdjustment.Id,
                CategoryName = timeAdjustment.Category.Name,
                Time = timeAdjustment.Time,
            };
        }

        public List<TimeAdjustmentDto> Map(List<TimeAdjustment> timeAdjustments)
        {
            return timeAdjustments.Select(x => Map(x)).ToList();
        }
    }
}