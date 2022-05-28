using FunTrophy.API.Contracts.Mappers;
using FunTrophy.Infrastructure.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Mappers
{
    public class TrackTimeMapper : ITrackTimeMapper
    {
        private readonly ITrackMapper _trackMapper;
        private readonly ITeamMapper _teamMapper;

        public TrackTimeMapper(ITrackMapper trackMapper, ITeamMapper teamMapper)
        {
            _trackMapper = trackMapper;
            _teamMapper = teamMapper;
        }

        public TeamLapInfoDto Map(TrackTime? current, TrackTime? next, Team team)
        {
            TrackDto? currentTrack = current?.Track == null ? null : _trackMapper.Map(current.Track);
            TrackDto? nextTrack = next?.Track == null ? null : _trackMapper.Map(next.Track);
            var mappedTeam = _teamMapper.Map(team);
            var currentStartTime = current?.StartTime;

            var lap = new TeamLapInfoDto
            {
                CurrentTrack = currentTrack,
                NextTrack = nextTrack,
                Team = mappedTeam,
                CurrentTrackStartTime = currentStartTime
            };
            return lap;
        }
    }
}
