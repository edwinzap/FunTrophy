using FunTrophy.Shared.Model;

namespace FunTrophy.API.Services.Contracts
{
    public interface ITeamService
    {
        Task<List<TeamDto>> GetAll();

        Task<int> Create(AddTeamDto team);

        Task Remove(int teamId);

        Task Update(int teamId, UpdateTeamDto team);
    }
}
