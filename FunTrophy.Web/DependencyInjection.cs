using FunTrophy.Web.Contracts.Services;
using FunTrophy.Web.Services;

namespace FunTrophy.Web
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddHttpClient<IRaceService, RaceService>(client =>
                {
                    client.BaseAddress = new Uri("https://localhost:7183/");
                });
            return services;
        }
    }
}
