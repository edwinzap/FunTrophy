using FunTrophy.Web.Contracts.Services;
using Microsoft.Extensions.Options;

namespace FunTrophy.Web.Services
{
    public class ExportService : ServiceBase, IExportService
    {
        private readonly AppSettings _settings;

        public ExportService(HttpClient httpClient, IOptions<AppSettings> settings) : base(httpClient, "export")
        {
            _settings = settings.Value;
        }

        public string GetTeamsByTimeAdjustmentCategoryFileUrl(int raceId)
        {
            return _settings.ApiUrl + GetUrl("categories", "raceId", raceId);
        }

        public string GetTimeAdjustmentCategoriesByColor(int raceId)
        {
            return _settings.ApiUrl + GetUrl("categoriesByColor", "raceId", raceId);
        }

        public string GetTracksByColorFileUrl(int raceId)
        {
            return _settings.ApiUrl + GetUrl("colors", "raceId", raceId);
        }
    }
}
