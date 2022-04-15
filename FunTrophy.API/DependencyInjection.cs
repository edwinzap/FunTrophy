using FunTrophy.API.Mappers;
using FunTrophy.API.Services;

namespace FunTrophy.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IRaceService, RaceService>();
            return services;
        }

        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddTransient<IMapper, Mapper>();
            return services;
        }
    }
}
