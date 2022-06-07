using FunTrophy.Web.Authentication;
using FunTrophy.Web.Contracts.Helpers;
using FunTrophy.Web.Contracts.Services;
using FunTrophy.Web.Helpers;
using FunTrophy.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace FunTrophy.Web
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddHttpClient<HttpClient>(HttpClientConfig());
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


        public static IServiceCollection AddHelpers(this IServiceCollection services)
        {
            services.AddTransient<INotificationHubHelper, NotificationHubHelper>();
            return services;
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthenticationService, AuthenticationService>(HttpClientConfig());
            services.AddHttpClient<AuthenticationStateProvider, AuthStateProvider>(HttpClientConfig());
            services.AddAuthorizationCore();

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
