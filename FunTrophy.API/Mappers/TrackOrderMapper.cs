using FunTrophy.API.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Mappers
{
    public class TrackOrderMapper : ITrackOrderMapper
    {
        private readonly ITrackMapper _trackMapper;

        public TrackOrderMapper(ITrackMapper trackMapper)
        {
            _trackMapper = trackMapper;
        }

        public TrackOrder Map(AddTrackOrderDto trackOrder)
        {
            return new TrackOrder
            {
                ColorId = trackOrder.ColorId,
                TrackId = trackOrder.TrackId,
                SortOrder = trackOrder.SortOrder,
            };
        }

        public TrackOrderDto Map(TrackOrder trackOrder)
        {
            return new TrackOrderDto
            {
                Id = trackOrder.Id,
                SordOrder = trackOrder.SortOrder,
                Track = _trackMapper.Map(trackOrder.Track)
            };
        }
    }
}