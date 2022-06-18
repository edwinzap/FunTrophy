using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;

namespace FunTrophy.Web.Services
{
    public class TimeAdjustmentService : ServiceBase, ITimeAdjustmentService
    {
        public TimeAdjustmentService(HttpClient httpClient) : base(httpClient, "timeadjustment")
        {
        }

        public async Task Add(AddTimeAdjustmentDto timeAdjustment)
        {
            var url = GetUrl();
            await PostAsync(url, timeAdjustment); 
        }

        public async Task<List<TeamTimeAdjustmentDto>> GetTimeAdjustmentForCategory(int categoryId)
        {
            var url = GetUrl() + "/bycategory/" + categoryId;
            return await GetAsync<List<TeamTimeAdjustmentDto>>(url);
        }

        public async Task<List<TimeAdjustmentDto>> GetTimeAdjustments(int teamId)
        {
            var url = GetUrl() + "/byteam/" + teamId;
            return await GetAsync<List<TimeAdjustmentDto>>(url);
        }

        public Task Remove(int timeAdjustmentId)
        {
            var url = GetUrl() + "/" + timeAdjustmentId;
            return DeleteAsync(url);
        }

        public Task Update(AddTimeAdjustmentDto updateTimeAdjustment)
        {
            var url = GetUrl();
            return UpdateAsync(url, updateTimeAdjustment);
        }
    }
}