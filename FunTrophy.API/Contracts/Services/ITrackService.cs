using FunTrophy.Shared.Model;

namespace FunTrophy.API.Services.Contracts
{
    public interface ITrackService
    {
        Task<List<TrackDto>> GetAll(int raceId);

        Task<int> Create(AddTrackDto track);

        Task Remove(int trackId);

        Task Update(int trackId, UpdateTrackDto track);
    }
}
