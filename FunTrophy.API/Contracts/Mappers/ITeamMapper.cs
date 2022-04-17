using FunTrophy.API.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Mappers
{
    public interface ITeamMapper
    {
        Team Map(AddTeamDto team);

        TeamDto Map(Team team);
    }
}