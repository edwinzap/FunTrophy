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

        public Task<List<RaceDto>> GetRaces()
        {
            var url = GetUrl();
            return GetAsync<List<RaceDto>>(url);
        }

        public Task Remove(int raceId)
        {
            throw new NotImplementedException();
        }
    }
}
