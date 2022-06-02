using FunTrophy.Shared.Model;

namespace FunTrophy.Web.Contracts.Services
{
    public interface IResultService
    {
        Task<List<TrackResultDto>> GetTrackResults(int trackId);

        Task<List<TeamResultDto>> GetTeamResults(int teamId);

        Task<List<FinalResultDto>> GetFinalResults(int raceId);
    }
}