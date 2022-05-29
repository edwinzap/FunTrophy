using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;

namespace FunTrophy.Web.Services
{
    public class ResultService : ServiceBase, IResultService
    {
        public ResultService(HttpClient httpClient) : base(httpClient, "Result")
        {
        }

        public Task<List<TeamResultDto>> GetTeamResults(int teamId)
        {
            var url = GetUrl() + $"/Team/{teamId}";
            return GetAsync<List<TeamResultDto>>(url);
        }

        public Task<List<TrackResultDto>> GetTrackResults(int trackId)
        {
            var url = GetUrl() + $"/Track/{trackId}";
            return GetAsync<List<TrackResultDto>>(url);
        }
    }
}
