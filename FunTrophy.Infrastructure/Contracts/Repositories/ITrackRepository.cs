using FunTrophy.Infrastructure.Model;

namespace FunTrophy.Infrastructure.Contracts.Repositories
{
    public interface ITrackRepository : IRepositoryBase<Track>
    {
        Task<List<Track>> GetOfRace(int raceId);
    }
}