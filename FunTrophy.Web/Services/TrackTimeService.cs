using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;

namespace FunTrophy.Web.Services
{
    public class TrackTimeService : ServiceBase, ITrackTimeService
    {
        public TrackTimeService(HttpClient httpClient) : base(httpClient, "tracktime")
        {
        }

        public Task<List<TeamLapInfoDto>> GetLaps(int colorId)
        {
            var url = GetUrl("colorId", colorId);
            return GetAsync<List<TeamLapInfoDto>>(url);
        }

        public Task SaveLap(int teamId)
        {
            var url = GetUrl("teamId", teamId);
            return PostAsync(url, null);
        }
    }
}
