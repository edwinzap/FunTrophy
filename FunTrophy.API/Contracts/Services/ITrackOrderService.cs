using FunTrophy.Shared.Model;

namespace FunTrophy.API.Contracts.Services
{
    public interface ITrackOrderService
    {
        Task<List<TrackOrderDto>> GetAll(int colorId);

        Task<int> Create(AddTrackOrderDto trackOrder);

        Task Remove(int trackOrderId);

        Task Update(int trackId, int sortOrder);
    }
}
