using AutoMapper;
using ImproveTeam.Domain.Interfaces;
using ImproveTeam.Infrastructure.DataAccess.EF;
using ImproveTeam.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ImproveTeam.Configurations
{
    public static class ServicesProfile
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new MappingProfile());
            });

            services.AddSingleton(mapperConfig.CreateMapper());

            services.AddScoped<IDbContext, DataStorageContext>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAdvertiserService, AdvertiserService>();
            services.AddScoped<IInterestsService, InterestsService>();
        }
    }
}
