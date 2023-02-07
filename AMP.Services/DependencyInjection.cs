using AMP.Processors.Messaging;
using AMP.Processors.Repositories;
using AMP.Processors.Services.Payments;
using AMP.Services.Authentication;
using AMP.Services.Images;
using AMP.Services.Messaging;
using AMP.Services.Payments;
using Microsoft.Extensions.DependencyInjection;

namespace AMP.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddSingleton<IAuthService, AuthService>()
                .AddScoped<IPaymentService, PaymentService>()
                .AddScoped<ICloudStorageService, CloudinaryService>()
                .AddSmsMessaging()
                .Configure<CloudinaryOptions>(options 
                    => configuration.GetSection("CloudinaryOptions").Bind(options));
            
            return services;
        }
        
        private static IServiceCollection AddSmsMessaging(this IServiceCollection services)
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