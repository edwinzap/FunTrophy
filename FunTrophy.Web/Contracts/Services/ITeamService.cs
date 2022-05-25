using FunTrophy.Shared.Model;

namespace FunTrophy.Web.Contracts.Services
{
    public interface ITeamService
    {
        Task Add(AddTeamDto team);

        Task<List<TeamDto>> GetTeams(int raceId);

        Task Remove(int teamId);

        Task Update(int teamId, UpdateTeamDto team);
    }
}