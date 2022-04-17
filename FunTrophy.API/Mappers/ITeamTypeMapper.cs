
namespace FunTrophy.API.Mappers
{
    public interface ITeamTypeMapper
    {
        Model.TeamType Map(Shared.Model.TeamType type);
        Shared.Model.TeamType Map(Model.TeamType type);
    }
}