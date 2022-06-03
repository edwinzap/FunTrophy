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

        public FinalResultDto MapFinal(Team team, List<TrackTime> trackTimes, List<TimeAdjustment> timeAdjustments)
        {
            var sortedTrackTimes = trackTimes.OrderBy(x => x.StartTime);
            var firstTrackTime = sortedTrackTimes.First();
            var lastTrackTime = sortedTrackTimes.Last();

            if (!firstTrackTime.StartTime.HasValue || !lastTrackTime.EndTime.HasValue)
                throw new InvalidDataException();

            var result = new FinalResultDto
            {
                Team = _teamMapper.Map(team),
                TracksTotalDuration = lastTrackTime.EndTime.Value.Subtract(firstTrackTime.StartTime.Value),
                TimeAdjustmentsTotalDuration = TimeSpan.FromSeconds(timeAdjustments.Sum(x => x.Seconds)),
            };
            return result;
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
