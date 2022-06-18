using FunTrophy.Infrastructure.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Contracts.Mappers
{
    public interface ITimeAdjustmentMapper
    {
        TimeAdjustment Map(AddTimeAdjustmentDto timeAdjustment);

        TimeAdjustmentDto Map(TimeAdjustment teamtimeAdjustment);

        List<TimeAdjustmentDto> Map(List<TimeAdjustment> timeAdjustments);

        List<TeamTimeAdjustmentDto> MapTeamTimeAdjustment(List<TimeAdjustment> timeAdjustments, List<Team> teams);
    }
}