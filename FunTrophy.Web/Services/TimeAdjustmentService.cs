using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;

namespace FunTrophy.Web.Services
{
    public class TimeAdjustmentService : ServiceBase, ITimeAdjustmentService
    {
        public TimeAdjustmentService(HttpClient httpClient) : base(httpClient, "TimeAdjustment")
        {
        }

        public async Task Add(AddTimeAdjustmentDto timeAdjustment)
        {
            var url = GetUrl();
            await PostAsync(url, timeAdjustment); 
        }

        public async Task<List<TimeAdjustmentDto>> GetTimeAdjustments(int teamId)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "teamId", teamId }
            };
            var url = GetUrl(parameters);
            return await GetAsync<List<TimeAdjustmentDto>>(url);
        }

        public Task Remove(int timeAdjustmentId)
        {
            var url = GetUrl() + "/" + timeAdjustmentId;
            return DeleteAsync(url);
        }
    }
}