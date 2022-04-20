using FunTrophy.Shared.Model;

namespace FunTrophy.API.Services.Contracts
{
    public interface ITrackOrderService
    {
        Task<List<TrackOrderDto>> GetAll(int colorId);

        Task<int> Create(AddTrackOrderDto trackOrder);

        Task Update(int trackId, int sortOrder);
    }
}
