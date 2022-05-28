using FunTrophy.API.Contracts.Mappers;
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
                Seconds = timeAdjustment.Seconds,
            };
        }

        public TimeAdjustmentDto Map(TimeAdjustment timeAdjustment)
        {
            return new TimeAdjustmentDto
            {
                Id = timeAdjustment.Id,
                CategoryName = timeAdjustment.Category.Name,
                Seconds = timeAdjustment.Seconds,
            };
        }

        public List<TimeAdjustmentDto> Map(List<TimeAdjustment> timeAdjustments)
        {
            return timeAdjustments.Select(x => Map(x)).ToList();
        }
    }
}