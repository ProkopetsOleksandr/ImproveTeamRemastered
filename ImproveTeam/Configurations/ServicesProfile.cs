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
            services.AddScoped<IDbContext, DataStorageContext>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
