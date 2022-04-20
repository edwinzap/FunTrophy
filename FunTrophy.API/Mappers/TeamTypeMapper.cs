using ApiTeamType = FunTrophy.Infrastructure.Model.TeamType;
using TeamType = FunTrophy.Shared.Model.TeamType;

namespace FunTrophy.API.Mappers
{
    public class TeamTypeMapper : ITeamTypeMapper
    {
        public ApiTeamType Map(TeamType type)
        {
            return type switch
            {
                TeamType.Family => ApiTeamType.Family,
                TeamType.Warrior => ApiTeamType.Warrior,
                _ => throw new NotImplementedException("This type is not handled")
            };
        }

        public TeamType Map(ApiTeamType type)
        {
            return type switch
            {
                ApiTeamType.Family => TeamType.Family,
                ApiTeamType.Warrior => TeamType.Warrior,
                _ => throw new NotImplementedException("This type is not handled")
            };
        }
    }
}