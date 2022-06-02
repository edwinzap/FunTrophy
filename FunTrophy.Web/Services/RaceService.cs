using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;

namespace FunTrophy.Web.Services
{
    public class RaceService : ServiceBase, IRaceService
    {
        public RaceService(HttpClient httpClient) : base(httpClient, "Race")
        {
        }

        public async Task Add(AddOrUpdateRaceDto race)
        {
            var url = GetUrl();
            await PostAsync(url, race);
        }

        public Task End(int raceId, bool isEnded)
        {
            var url = GetUrl() + $"/{raceId}/end/{isEnded}";
            return UpdateAsync(url, null);
        }

        public Task<RaceDto> GetRace(int raceId)
        {
            var url = GetUrl() + "/" + raceId;
            return GetAsync<RaceDto>(url);
        }

        public async Task<List<RaceDto>> GetRaces()
        {
            var url = GetUrl();
            return await GetAsync<List<RaceDto>>(url);
        }

        public Task Remove(int raceId)
        {
            var url = GetUrl() + "/" + raceId;
            return DeleteAsync(url);
        }

        public Task Update(int raceId, AddOrUpdateRaceDto race)
        {
            var url = GetUrl() + "/" + raceId;
            return UpdateAsync(url, race);
        }
    }
}