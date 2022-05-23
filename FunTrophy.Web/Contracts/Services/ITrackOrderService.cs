using FunTrophy.Shared.Model;

namespace FunTrophy.Web.Contracts.Services
{
    internal interface ITrackOrderService
    {
        Task<List<TrackDto>> GetTrackOrders(int colorId);

        Task Update(UpdateTracksOrderDto trackOrders);
    }
}