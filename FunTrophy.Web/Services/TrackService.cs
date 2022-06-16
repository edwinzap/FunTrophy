using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;

namespace FunTrophy.Web.Services
{
    public class TrackService : ServiceBase, ITrackService
    {
        public TrackService(HttpClient httpClient) : base(httpClient, "track")
        {
        }

        public async Task Add(AddTrackDto track)
        {
            var url = GetUrl();
            await PostAsync(url, track); 
        }

        public async Task<List<TrackDto>> GetTracks(int raceId)
        {
            var url = GetUrl("raceId", raceId);
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