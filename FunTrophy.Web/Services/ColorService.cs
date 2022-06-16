using FunTrophy.Shared.Model;
using FunTrophy.Web.Contracts.Services;

namespace FunTrophy.Web.Services
{
    public class ColorService : ServiceBase, IColorService
    {
        public ColorService(HttpClient httpClient) : base(httpClient, "color")
        {
        }

        public async Task Add(AddColorDto color)
        {
            var url = GetUrl();
            await PostAsync(url, color); 
        }

        public async Task<List<ColorDto>> GetColors(int raceId)
        {
            var url = GetUrl("raceId", raceId);
            return await GetAsync<List<ColorDto>>(url);
        }

        public Task Remove(int colorId)
        {
            var url = GetUrl() + "/" + colorId;
            return DeleteAsync(url);
        }

        public Task Update(int colorId, UpdateColorDto color)
        {
            var url = GetUrl() + "/" + colorId;
            return UpdateAsync(url, color);
        }
    }
}