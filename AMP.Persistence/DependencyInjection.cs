using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AMP.Persistence.Database;
using AMP.Processors;
using AMP.Processors.ExceptionHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace AMP.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AmpDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DevDb"));
            });
            return services;
        }

        /// <summary>
        ///    Registers all repositories in the assembly.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        /// <exception cref="RepositoryNotFoundException"></exception>
        public static IServiceCollection AddRepositories(this IServiceCollection services, ILogger logger)
        {
            var definedTypes = typeof(DependencyInjection).Assembly.DefinedTypes;
            var repositories = definedTypes
                .Where(t => t.IsClass && t.GetCustomAttribute<RepositoryAttribute>() != null);

            try
            {
                Parallel.ForEach(repositories, repository =>
                {
                    var irepository = repository.GetInterfaces().FirstOrDefault(i => i.Name == $"I{repository.Name}") ??
                                      throw new RepositoryNotFoundException(
                                          $"{repository.Name} has no interface with name I{repository.Name}");
                    services.AddScoped(irepository, repository);
                });
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }

            return services;
        }
    }
}