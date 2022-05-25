using FunTrophy.Shared.Model;

namespace FunTrophy.Web.Contracts.Services
{
    public interface ITrackService
    {
        Task Add(AddTrackDto track);

        Task<List<TrackDto>> GetTracks(int raceId);

        Task Remove(int trackId);

        Task Update(int trackId, UpdateTrackDto track);
    }
}