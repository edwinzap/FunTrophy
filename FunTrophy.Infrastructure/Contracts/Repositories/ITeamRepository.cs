using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Infrastructure.Contracts.Repositories
{
    public interface ITeamRepository : IRepositoryBase<Team>
    {
        Task<List<Team>> GetOfColor(int colorId);
        Task<List<Team>> GetOfRace(int raceId);
    }
}