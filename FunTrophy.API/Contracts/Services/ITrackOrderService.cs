using FunTrophy.Shared.Model;

namespace FunTrophy.API.Contracts.Services
{
    public interface ITrackOrderService
    {
        Task<List<TrackOrderDto>> GetAll(int colorId);

        Task Update(UpdateTracksOrderDto tracksOrder);

    }
}
