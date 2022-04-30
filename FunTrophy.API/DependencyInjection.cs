using FunTrophy.API.Mappers;
using FunTrophy.API.Services;
using FunTrophy.API.Contracts.Services;
using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Repositories;

namespace FunTrophy.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IColorService, ColorService>();
            services.AddTransient<IRaceService, RaceService>();
            services.AddTransient<ITeamService, TeamService>();
            services.AddTransient<ITrackOrderService, TrackOrderService>();
            services.AddTransient<ITrackService, TrackService>();
            services.AddTransient<ITimeAdjustmentService, TimeAdjustmentService>();
            services.AddTransient<ITimeAdjustmentCategoryService, TimeAdjustmentCategoryService>();
            return services;
        }

        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddTransient<IColorMapper, ColorMapper>();
            services.AddTransient<IRaceMapper, RaceMapper>();
            services.AddTransient<ITeamMapper, TeamMapper>();
            services.AddTransient<ITeamTypeMapper, TeamTypeMapper>();
            services.AddTransient<ITrackMapper, TrackMapper>();
            services.AddTransient<ITrackOrderMapper, TrackOrderMapper>();
            services.AddTransient<ITimeAdjustmentMapper, TimeAdjustmentMapper>();
            services.AddTransient<ITimeAdjustmentCategoryMapper, TimeAdjustmentCategoryMapper>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IColorRepository, ColorRepository>();
            services.AddTransient<IRaceRepository, RaceRepository>();
            services.AddTransient<ITeamRepository, TeamRepository>();
            services.AddTransient<ITrackOrderRepository, TrackOrderRepository>();
            services.AddTransient<ITrackRepository, TrackRepository>();
            services.AddTransient<ITimeAdjustmentRepository, TimeAdjustmentRepository>();
            services.AddTransient<ITimeAdjustmentCategoryRepository, TimeAdjustmentCategoryRepository>();
            return services;
        }
    }
}
