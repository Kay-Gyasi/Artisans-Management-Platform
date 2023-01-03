using System.Reflection;
using AMP.Processors.Workers;
using AMP.Processors.Workers.BackgroundWorker;
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

            var processors = definedTypes.Where(x => 
                x.IsClass && x.GetCustomAttribute(attribute) != null);
            foreach (var processor in processors)
            {
                services.AddScoped(processor.AsType());
            }

            services.RegisterAutoMapper()
                .AddCaching()
                .AddWorkers();
            return services;
        }

        private static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
        {
            
            var assemblies = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assemblies);
            return services;
        }

        private static IServiceCollection AddCaching(this IServiceCollection services)
        {
            services.AddMemoryCache();
            return services;
        }
        
        private static IServiceCollection AddWorkers(this IServiceCollection services)
        {
            services.AddHostedService<SmsService>();
            services.AddScoped<MessageGenerator>();
            services.AddScoped<IBackgroundWorker, BackgroundWorker>();
            services.Configure<HostOptions>(options =>
            {
                options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.StopHost;
            });
            return services;
        }
    }
}
