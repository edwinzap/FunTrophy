using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;

namespace FunTrophy.Web.Services
{
    public class TeamService : ServiceBase, ITeamService
    {
        public TeamService(HttpClient httpClient) : base(httpClient, "Team")
        {
        }

        public async Task Add(AddTeamDto team)
        {
            var url = GetUrl();
            await PostAsync(url, team);
        }
        public async Task<List<TeamDto>> GetTeamsByRace(int raceId)
        {
            var url = GetUrl() + $"/ByRace/{raceId}";
            return await GetAsync<List<TeamDto>>(url);
        }


        public async Task<List<TeamDto>> GetTeamsByColor(int colorId)
        {
            var url = GetUrl() + $"/ByColor/{colorId}";
            return await GetAsync<List<TeamDto>>(url);
        }

        public Task Remove(int teamId)
        {
            var url = GetUrl() + "/" + teamId;
            return DeleteAsync(url);
        }

        public Task Update(int teamId, UpdateTeamDto team)
        {
            var url = GetUrl() + "/" + teamId;
            return UpdateAsync(url, team);
        }
    }
}