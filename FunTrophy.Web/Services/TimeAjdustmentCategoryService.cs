using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;

namespace FunTrophy.Web.Services
{
    public class TimeAdjustmentCategoryService : ServiceBase, ITimeAdjustmentCategoryService
    {
        public TimeAdjustmentCategoryService(HttpClient httpClient) : base(httpClient, "TimeAdjustmentCategory")
        {
        }

        public async Task Add(AddTimeAdjustmentCategoryDto category)
        {
            var url = GetUrl();
            await PostAsync(url, category); 
        }

        public async Task<List<TimeAdjustmentCategoryDto>> GetCategories(int raceId)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "raceId", raceId }
            };
            var url = GetUrl(parameters);
            return await GetAsync<List<TimeAdjustmentCategoryDto>>(url);
        }

        public Task Remove(int categoryId)
        {
            var url = GetUrl() + "/" + categoryId;
            return DeleteAsync(url);
        }

        public Task Update(int categoryId, UpdateTimeAdjustmentCategoryDto category)
        {
            var url = GetUrl() + "/" + categoryId;
            return UpdateAsync(url, category);
        }
    }
}