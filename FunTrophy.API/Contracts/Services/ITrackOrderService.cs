using FunTrophy.Shared.Model;

namespace FunTrophy.API.Services.Contracts
{
    public interface ITrackOrderService
    {
        Task<List<TrackOrderDto>> GetAll(int colorId);

        Task<int> AddTrackOrder(AddTrackOrderDto trackOrder);

        Task UpdateTrackOrder(int trackId, int sortOrder);
    }
}
