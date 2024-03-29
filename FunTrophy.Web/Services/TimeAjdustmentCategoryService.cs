﻿using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;

namespace FunTrophy.Web.Services
{
    public class TimeAdjustmentCategoryService : ServiceBase, ITimeAdjustmentCategoryService
    {
        public TimeAdjustmentCategoryService(HttpClient httpClient) : base(httpClient, "timeadjustmentcategory")
        {
        }

        public async Task Add(AddTimeAdjustmentCategoryDto category)
        {
            var url = GetUrl();
            await PostAsync(url, category); 
        }

        public async Task<List<TimeAdjustmentCategoryDto>> GetCategories(int raceId)
        {
            var url = GetUrl("raceId", raceId);
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