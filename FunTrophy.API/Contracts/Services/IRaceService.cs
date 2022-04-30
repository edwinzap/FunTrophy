using FunTrophy.Shared.Model;

namespace FunTrophy.API.Contracts.Services
{
    public interface IRaceService
    {
        Task<RaceDto> Get(int raceId);

        Task<List<RaceDto>> GetAll();

        Task<int> Create(AddOrUpdateRaceDto race);

        Task Remove(int raceId);

        Task Update(int raceId, AddOrUpdateRaceDto race);
    }
}