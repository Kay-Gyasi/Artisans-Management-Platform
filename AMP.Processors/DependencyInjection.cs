using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace AMP.Processors
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProcessors(this IServiceCollection services)
        {
            var attribute = typeof(ProcessorAttribute);
            var assembly = attribute.Assembly;
            var definedTypes = assembly.DefinedTypes;

            var processors = definedTypes.Where(x => x.IsClass && x.GetCustomAttribute(attribute) != null).ToList();
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
    }
}
