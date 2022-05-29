using FunTrophy.API.Contracts.Mappers;
using FunTrophy.Infrastructure.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Mappers
{
    public class ResultMapper : IResultMapper
    {
        private readonly ITeamMapper _teamMapper;
        private readonly ITrackMapper _trackMapper;

        public ResultMapper(ITeamMapper teamMapper, ITrackMapper trackMapper)
        {
            _teamMapper = teamMapper;
            _trackMapper = trackMapper;
        }

        public TeamResultDto MapOfTeam(TrackTime trackTime)
        {
            return new TeamResultDto
            {
                Track = _trackMapper.Map(trackTime.Track),
                LapDuration = GetLapDuration(trackTime.StartTime, trackTime.EndTime)
            };
        }

        public List<TeamResultDto> MapOfTeam(List<TrackTime> trackTimes)
        {
            return trackTimes.Select(x => MapOfTeam(x)).ToList();
        }

        public TrackResultDto MapOfTrack(TrackTime trackTime)
        {
            return new TrackResultDto
            {
                Team = _teamMapper.Map(trackTime.Team),
                LapDuration = GetLapDuration(trackTime.StartTime, trackTime.EndTime)
            };
        }

        public List<TrackResultDto> MapOfTrack(List<TrackTime> trackTimes)
        {
            return trackTimes.Select(x => MapOfTrack(x)).ToList();
        }

        private TimeSpan? GetLapDuration(DateTime? startTime, DateTime? endTime)
        {
            if (startTime.HasValue && endTime.HasValue)
            {
                return endTime.Value.Subtract(startTime.Value);
            }
            return null;
        }
    }
}
