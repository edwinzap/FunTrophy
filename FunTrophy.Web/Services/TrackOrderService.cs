using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;

namespace FunTrophy.Web.Services
{
    public class TrackOrderService : ServiceBase, ITrackOrderService
    {
        public TrackOrderService(HttpClient httpClient) : base(httpClient, "TrackOrder")
        {
        }

        public async Task<List<TrackDto>> GetTrackOrders(int colorId)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "colorId", colorId }
            };
            var url = GetUrl(parameters);
            var trackOrders = await GetAsync<List<TrackOrderDto>>(url);
            var tracks = trackOrders.OrderBy(x => x.SortOrder).Select(x => x.Track).ToList();
            return tracks;
        }

        public Task Update(UpdateTracksOrderDto tracksOrder)
        {
            var url = GetUrl();
            return UpdateAsync(url, tracksOrder);
        }
    }
}