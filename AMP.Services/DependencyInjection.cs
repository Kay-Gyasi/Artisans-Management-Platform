using System;
using AMP.Processors.Authentication;
using AMP.Processors.Messaging;
using AMP.Processors.Repositories;
using AMP.Services.Authentication;
using AMP.Services.Images;
using AMP.Services.Messaging;
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
            services.Configure<CloudinaryOptions>(options 
                => configuration.GetSection("CloudinaryOptions").Bind(options));
            services.AddSingleton<ICloudStorageService, CloudinaryService>();
            return services;
        }
        
        public static IServiceCollection AddSmsMessaging(this IServiceCollection services)
        {
            services.AddScoped<ISmsMessaging, SmsMessaging>();
            services.AddHttpClient("SmsClient", options =>
            {
                options.BaseAddress = new Uri("https://sms.arkesel.com/api/v2/");
            });
            return services;
        }
    }
}