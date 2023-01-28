using ImproveTeam.Domain.Interfaces;
using ImproveTeam.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ImproveTeam.Configurations
{
    public static class ServicesProfile
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
