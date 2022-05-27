using FunTrophy.Infrastructure.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Contracts.Mappers
{
    public interface ITeamMapper
    {
        Team Map(AddTeamDto team);

        TeamDto Map(Team team);

        List<TeamDto> Map(List<Team> teams);
    }
}