using FunTrophy.API.Authentication;
using FunTrophy.API.Contracts.Helpers;
using FunTrophy.API.Contracts.Mappers;
using FunTrophy.API.Contracts.Services;
using FunTrophy.API.Helpers;
using FunTrophy.API.Mappers;
using FunTrophy.API.Services;
using FunTrophy.Fake;
using FunTrophy.Fake.Repositories;
using FunTrophy.Infrastructure.Contracts.Repositories;
using FunTrophy.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            services.AddTransient<ITrackTimeService, TrackTimeService>();
            services.AddTransient<IResultService, ResultService>();
            services.AddTransient<IUserService, UserService>();
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
            services.AddTransient<ITrackTimeMapper, TrackTimeMapper>();
            services.AddTransient<IResultMapper, ResultMapper>();
            services.AddTransient<IUserMapper, UserMapper>();
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
            services.AddTransient<ITrackTimeRepository, TrackTimeRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            return services;
        }

        public static IServiceCollection AddHelpers(this IServiceCollection services)
        {
            services.AddTransient<INotificationHelper, NotificationHelper>();
            return services;
        }

        public static IServiceCollection AddFakeRepositories(this IServiceCollection services)
        {
            services.AddSingleton<FakeDbContext>();
            services.AddTransient<IColorRepository, FakeColorRepository>();
            services.AddTransient<IRaceRepository, FakeRaceRepository>();
            services.AddTransient<ITeamRepository, FakeTeamRepository>();
            services.AddTransient<ITrackRepository, FakeTrackRepository>();
            services.AddTransient<ITrackOrderRepository, FakeTrackOrderRepository>();
            services.AddTransient<ITimeAdjustmentCategoryRepository, FakeTimeAdjustmentCategoryRepository>();
            services.AddTransient<ITimeAdjustmentRepository, FakeTimeAdjustmentRepository>();
            services.AddTransient<ITrackTimeRepository, FakeTrackTimeRepository>();
            return services;
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services, ConfigurationManager configuration)
        {
            // Add Authentication
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var Key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            });

            services.AddSingleton<IJWTManagerRepository, JWTManagerRepository>();
            return services;
        }
    }
}