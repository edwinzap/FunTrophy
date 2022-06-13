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
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<CustomAuthorizationHandler>();
            services.AddCustomHttpClient<IRaceService, RaceService>(config);
            services.AddCustomHttpClient<IColorService, ColorService>(config);
            services.AddCustomHttpClient<ITeamService, TeamService>(config);
            services.AddCustomHttpClient<ITrackService, TrackService>(config);
            services.AddCustomHttpClient<ITrackOrderService, TrackOrderService>(config);
            services.AddCustomHttpClient<ITimeAdjustmentCategoryService, TimeAdjustmentCategoryService>(config);
            services.AddCustomHttpClient<ITimeAdjustmentService, TimeAdjustmentService>(config);
            services.AddCustomHttpClient<ITrackTimeService, TrackTimeService>(config);
            services.AddCustomHttpClient<IResultService, ResultService>(config);
            services.AddCustomHttpClient<IUserService, UserService>(config);
            services.AddCustomHttpClient<IExportService, ExportService>(config);
            services.AddScoped<IAppStateService, AppStateService>();
            return services;
        }

        public static IServiceCollection AddHelpers(this IServiceCollection services)
        {
            services.AddTransient<INotificationHubHelper, NotificationHubHelper>();
            return services;
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.AddHttpClient<IAuthenticationService, AuthenticationService>(HttpClientConfig(config));
            services.AddScoped<AuthStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(provider =>
                provider.GetRequiredService<AuthStateProvider>());
            services.AddAuthorizationCore();

            return services;
        }

        public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<AppSettings>(options => config.Bind(options));
            return services;
        }

        private static IHttpClientBuilder AddCustomHttpClient<TInterface, TImplementation>(this IServiceCollection services, IConfiguration config)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            return services.AddHttpClient<TInterface, TImplementation>(HttpClientConfig(config))
                .AddHttpMessageHandler<CustomAuthorizationHandler>();
        }

        private static Action<IServiceProvider, HttpClient> HttpClientConfig(IConfiguration config)
        {
            var url = config.GetValue<string>("ApiUrl");
            return (provider, client) =>
            {
                client.BaseAddress = new Uri(url);
            };
        }
    }
}