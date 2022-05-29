using FunTrophy.Web.Contracts.Services;
using FunTrophy.Web.Services;

namespace FunTrophy.Web
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddHttpClient<IRaceService, RaceService>(HttpClientConfig());
            services.AddHttpClient<IColorService, ColorService>(HttpClientConfig());
            services.AddHttpClient<ITeamService, TeamService>(HttpClientConfig());
            services.AddHttpClient<ITrackService, TrackService>(HttpClientConfig());
            services.AddHttpClient<ITrackOrderService, TrackOrderService>(HttpClientConfig());
            services.AddHttpClient<ITimeAdjustmentCategoryService, TimeAdjustmentCategoryService>(HttpClientConfig());
            services.AddHttpClient<ITimeAdjustmentService, TimeAdjustmentService>(HttpClientConfig());
            services.AddHttpClient<ITrackTimeService, TrackTimeService>(HttpClientConfig());
            services.AddHttpClient<IResultService, ResultService>(HttpClientConfig());
           
            return services;
        }
        private static Action<IServiceProvider, HttpClient> HttpClientConfig()
        {
            return (provider, client) =>
            {
                client.BaseAddress = new Uri("https://localhost:7183/");
            };
        }
    }
}
