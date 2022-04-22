using FunTrophy.Infrastructure.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Mappers
{
    public class TrackMapper : ITrackMapper
    {
        public Track Map(AddTrackDto track)
        {
            return new Track
            {
                Name = track.Name,
                RaceId = track.RaceId,
            };
        }

        public TrackDto Map(Track track)
        {
            return new TrackDto
            {
                Id = track.Id,
                Name = track.Name,
            };
        }

        public List<TrackDto> Map(List<Track> tracks)
        {
            return tracks.Select(x => Map(x)).ToList();
        }
    }
}