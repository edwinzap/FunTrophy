using FunTrophy.API.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Mappers
{
    public class TeamMapper : ITeamMapper
    {
        private readonly ITeamTypeMapper _typeMapper;

        public TeamMapper(ITeamTypeMapper typeMapper)
        {
            _typeMapper = typeMapper;
        }

        public Team Map(AddTeamDto team)
        {
            return new Team
            {
                Name = team.Name,
                Number = team.Number,
                ColorId = team.ColorId,
                RaceId = team.RaceId,
            };
        }

        public TeamDto Map(Team team)
        {
            return new TeamDto
            {
                Id = team.Id,
                Name = team.Name,
                Number = team.Number,
                Type = _typeMapper.Map(team.Type)
            };
        }
    }
}
