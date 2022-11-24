using System.Reflection;
using AMP.Processors.Workers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AMP.Processors
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProcessors(this IServiceCollection services)
        {
            var attribute = typeof(ProcessorAttribute);
            var assembly = attribute.Assembly;
            var definedTypes = assembly.DefinedTypes;

            var processors = definedTypes.Where(x => x.IsClass && x.GetCustomAttribute(attribute) != null);
            foreach (var processor in processors)
            {
                services.AddScoped(processor.AsType());
            }
            return services;
        }

        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
        {
            
            var assemblies = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assemblies);
            return services;
        }

        public static IServiceCollection AddCaching(this IServiceCollection services)
        {
            services.AddMemoryCache();
            return services;
        }
        
        public static IServiceCollection AddWorkers(this IServiceCollection services)
        {
            services.AddHostedService<SmsService>();
            services.AddScoped<MessageGenerator>();
            services.Configure<HostOptions>(options =>
            {
                options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.StopHost;
            });
            return services;
        }
    }
}
