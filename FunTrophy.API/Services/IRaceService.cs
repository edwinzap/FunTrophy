using FunTrophy.Shared.Model;

namespace FunTrophy.API.Services
{
    public interface IRaceService
    {
        Task<List<RaceDto>> GetAllRaces();

        Task<int> CreateRace(AddOrUpdateRaceDto race);

        Task<RaceDto> GetRace(int raceId);

        Task RemoveRace(int raceId);

        Task UpdateRace(int raceId, AddOrUpdateRaceDto race);
    }
}