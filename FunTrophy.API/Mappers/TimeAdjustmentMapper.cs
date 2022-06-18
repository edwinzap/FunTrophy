using FunTrophy.API.Contracts.Mappers;
using FunTrophy.Infrastructure.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Mappers
{
    public class TimeAdjustmentMapper : ITimeAdjustmentMapper
    {
        private readonly ITeamMapper _teamMapper;

        public TimeAdjustmentMapper(ITeamMapper teamMapper)
        {
            _teamMapper = teamMapper;
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

        public List<TeamTimeAdjustmentDto> MapTeamTimeAdjustment(List<TimeAdjustment> timeAdjustments, List<Team> teams)
        {
            var mappedTimeAdjustments = teams.Select(team => new TeamTimeAdjustmentDto
            {
                Team = _teamMapper.Map(team),
                Seconds = timeAdjustments.FirstOrDefault(x => x.TeamId == team.Id)?.Seconds
            });
            return mappedTimeAdjustments.ToList();
        }
    }
}