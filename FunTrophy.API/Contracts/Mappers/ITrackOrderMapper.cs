using FunTrophy.Infrastructure.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Contracts.Mappers
{
    public interface ITrackOrderMapper
    {
        TrackOrderDto Map(int colorId, TrackOrder? trackOrder, Track track);

        List<TrackOrderDto> Map(int colorId, List<TrackOrder> trackOrders, List<Track> tracks);
    }
}