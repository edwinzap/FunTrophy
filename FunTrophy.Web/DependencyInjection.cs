using FunTrophy.Web.Authentication;
using FunTrophy.Web.Contracts.Helpers;
using FunTrophy.Web.Contracts.Services;
using FunTrophy.Web.Helpers;
using FunTrophy.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace FunTrophy.Web
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<CustomAuthorizationHandler>();
            services.AddCustomHttpClient<IRaceService, RaceService>();
            services.AddCustomHttpClient<IColorService, ColorService>();
            services.AddCustomHttpClient<ITeamService, TeamService>();
            services.AddCustomHttpClient<ITrackService, TrackService>();
            services.AddCustomHttpClient<ITrackOrderService, TrackOrderService>();
            services.AddCustomHttpClient<ITimeAdjustmentCategoryService, TimeAdjustmentCategoryService>();
            services.AddCustomHttpClient<ITimeAdjustmentService, TimeAdjustmentService>();
            services.AddCustomHttpClient<ITrackTimeService, TrackTimeService>();
            services.AddCustomHttpClient<IResultService, ResultService>();
            services.AddCustomHttpClient<IUserService, UserService>();
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
            services.AddScoped<AuthStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(provider =>
                provider.GetRequiredService<AuthStateProvider>());
            services.AddAuthorizationCore();

            return services;
        }

        private static IHttpClientBuilder AddCustomHttpClient<TInterface, TImplementation>(this IServiceCollection services)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            return services.AddHttpClient<TInterface, TImplementation>(HttpClientConfig())
                .AddHttpMessageHandler<CustomAuthorizationHandler>();
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