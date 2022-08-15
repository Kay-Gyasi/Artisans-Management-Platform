using AMP.Processors.Authentication;
using AMP.Processors.Payment;
using AMP.Processors.Repositories;
using AMP.Services.Authentication;
using AMP.Services.Images;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AMP.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddSingleton<IAuthService, AuthService>();
            //services.AddSingleton<IPaymentService, PaymentService>();
            services.Configure<CloudinaryOptions>(options 
                => configuration.GetSection("CloudinaryOptions").Bind(options));
            services.AddSingleton<ICloudinaryService, CloudinaryService>();
            return services;
        }
    }
}