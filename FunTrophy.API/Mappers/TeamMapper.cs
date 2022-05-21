using FunTrophy.Infrastructure.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Mappers
{
    public class TeamMapper : ITeamMapper
    {
        private readonly ITeamTypeMapper _typeMapper;
        private readonly IColorMapper _colorMapper;

        public TeamMapper(ITeamTypeMapper typeMapper, IColorMapper colorMapper)
        {
            _typeMapper = typeMapper;
            _colorMapper = colorMapper;
        }

        public Team Map(AddTeamDto team)
        {
            return new Team
            {
                Name = team.Name,
                Number = team.Number,
                ColorId = team.ColorId,
            };
        }

        public TeamDto Map(Team team)
        {
            return new TeamDto
            {
                Id = team.Id,
                Name = team.Name,
                Number = team.Number,
                Color = _colorMapper.Map(team.Color),
                Type = _typeMapper.Map(team.Type)
            };
        }

        public List<TeamDto> Map(List<Team> teams)
        {
            return teams.Select(x => Map(x)).ToList();
        }
    }
}