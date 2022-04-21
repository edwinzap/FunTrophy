using ApiTeamType = FunTrophy.Infrastructure.Model.TeamType;
using TeamType = FunTrophy.Shared.Model.TeamType;

namespace FunTrophy.API.Mappers
{
    public interface ITeamTypeMapper
    {
        TeamType Map(ApiTeamType type);

        ApiTeamType Map(TeamType type);
    }
}