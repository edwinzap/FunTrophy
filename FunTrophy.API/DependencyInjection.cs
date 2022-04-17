using FunTrophy.API.Mappers;
using FunTrophy.API.Services;
using FunTrophy.API.Services.Contracts;

namespace FunTrophy.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IRaceService, RaceService>();
            services.AddTransient<IColorService, ColorService>();
            return services;
        }

        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddTransient<IRaceMapper, RaceMapper>();
            services.AddTransient<IColorMapper, ColorMapper>();
            return services;
        }
    }
}
