using FunTrophy.Shared.Model;

namespace FunTrophy.Web.Contracts.Services
{
    public interface IRaceService
    {
        Task<List<RaceDto>> GetRaces();

        Task Remove(int raceId);

        Task Add(AddOrUpdateRaceDto race);

        Task Update(AddOrUpdateRaceDto race);
    }
}