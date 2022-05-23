using FunTrophy.Infrastructure.Model;
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

        public TrackOrderDto Map(int colorId, TrackOrder? trackOrder, Track track)
        {
            if (trackOrder != null && trackOrder.TrackId != track.Id)
                throw new InvalidDataException();

            return new TrackOrderDto
            {
                ColorId = colorId,
                SortOrder = trackOrder?.SortOrder,
                Track = _trackMapper.Map(track)
            };
        }

        public List<TrackOrderDto> Map(int colorId, List<TrackOrder> trackOrders, List<Track> tracks)
        {
            var mappedTrackOrders = tracks
                .Select(track =>
                {
                    var trackOrder = trackOrders.FirstOrDefault(x => x.TrackId == track.Id);
                    return Map(colorId, trackOrder, track);
                })
                .OrderBy(x => x.SortOrder)
                .ToList();
            return mappedTrackOrders;
        }
    }
}