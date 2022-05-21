using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;

namespace FunTrophy.Web.Services
{
    public class TrackService : ServiceBase, ITrackService
    {
        public TrackService(HttpClient httpClient) : base(httpClient, "Track")
        {
        }

        public async Task Add(AddTrackDto track)
        {
            var url = GetUrl();
            await PostAsync(url, track); 
        }

        public async Task<List<TrackDto>> GetTracks(int raceId)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "raceId", raceId }
            };
            var url = GetUrl(parameters);
            return await GetAsync<List<TrackDto>>(url);
        }

        public Task Remove(int trackId)
        {
            var url = GetUrl() + "/" + trackId;
            return DeleteAsync(url);
        }

        public Task Update(int trackId, UpdateTrackDto track)
        {
            var url = GetUrl() + "/" + trackId;
            return UpdateAsync(url, track);
        }
    }
}