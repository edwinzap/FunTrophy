using FunTrophy.Shared.Model;

namespace FunTrophy.API.Contracts.Services
{
    public interface ITeamService
    {
        Task<List<TeamDto>> GetByRace(int raceId);

        Task<List<TeamDto>> GetByColor(int colorId);

        Task<int> Create(AddTeamDto team);

        Task Remove(int teamId);

        Task Update(int teamId, UpdateTeamDto team);
    }
}