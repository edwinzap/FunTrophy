using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;

namespace FunTrophy.Web.Services
{
    public class TrackTimeService : ServiceBase, ITrackTimeService
    {
        public TrackTimeService(HttpClient httpClient) : base(httpClient, "TrackTime")
        {
        }

        public Task<List<TeamLapInfoDto>> GetLaps(int colorId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "colorId", colorId }
            };
            var url = GetUrl(parameters);
            return GetAsync<List<TeamLapInfoDto>>(url);
        }

        public Task<TeamLapInfoDto> SaveLap(int teamId)
        {
            throw new NotImplementedException();
        }
    }
}
