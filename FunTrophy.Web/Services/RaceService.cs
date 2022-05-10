using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;

namespace FunTrophy.Web.Services
{
    public class RaceService : ServiceBase, IRaceService
    {
        public RaceService(HttpClient httpClient) : base(httpClient, "Race")
        {
        }

        public Task Add(AddOrUpdateRaceDto race)
        {
            throw new NotImplementedException();
        }

        public async Task<List<RaceDto>> GetRaces()
        {
            return FakeModel.Races;
            var url = GetUrl();
            return await GetAsync<List<RaceDto>>(url);
        }

        public Task Remove(int raceId)
        {
            throw new NotImplementedException();
        }
    }
}