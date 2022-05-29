using FunTrophy.Shared.Model;

namespace FunTrophy.API.Contracts.Services
{
    public interface IResultService
    {
        Task<List<TrackResultDto>> GetTrackResults(int trackId);

        Task<List<TeamResultDto>> GetTeamResults(int teamId);
    }
}