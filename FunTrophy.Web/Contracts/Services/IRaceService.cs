using FunTrophy.Shared.Model;

namespace FunTrophy.Web.Contracts.Services
{
    public interface IRaceService
    {
        Task<RaceDto> GetRace(int raceId);

        Task<List<RaceDto>> GetRaces();

        Task Remove(int raceId);

        Task Add(AddOrUpdateRaceDto race);

        Task Update(int raceId, AddOrUpdateRaceDto race);

        Task End(int raceId, bool isEnded);
    }
}