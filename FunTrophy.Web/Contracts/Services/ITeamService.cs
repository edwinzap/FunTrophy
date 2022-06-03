using FunTrophy.Shared.Model;

namespace FunTrophy.Web.Contracts.Services
{
    public interface ITeamService
    {
        Task Add(AddTeamDto team);

        Task<List<TeamDto>> GetTeamsByRace(int raceId);

        Task<List<TeamDto>> GetTeamsByColor(int colorId);

        Task Remove(int teamId);

        Task Update(int teamId, UpdateTeamDto team);
    }
}