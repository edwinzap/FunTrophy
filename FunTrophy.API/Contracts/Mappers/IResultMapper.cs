using FunTrophy.Infrastructure.Model;
using FunTrophy.Shared.Model;

namespace FunTrophy.API.Contracts.Mappers
{
    public interface IResultMapper
    {
        TrackResultDto MapOfTrack(TrackTime trackTime);

        List<TrackResultDto> MapOfTrack(List<TrackTime> trackTimes);

        TeamResultDto MapOfTeam(TrackTime trackTime);

        List<TeamResultDto> MapOfTeam(List<TrackTime> trackTimes);

        FinalResultDto MapFinal(Team team, List<TrackTime> trackTimes, List<TimeAdjustment> timeAdjustments);
    }
}