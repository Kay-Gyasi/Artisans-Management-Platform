using AMP.Processors.Authentication;
using AMP.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace AMP.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IAuthService, AuthService>();
            return services;
        }
    }
}