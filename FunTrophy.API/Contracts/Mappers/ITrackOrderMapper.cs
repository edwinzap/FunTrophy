using FunTrophy.API.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Mappers
{
    public interface ITrackOrderMapper
    {
        TrackOrder Map(AddTrackOrderDto trackOrder);

        TrackOrderDto Map(TrackOrder trackOrder);
    }
}