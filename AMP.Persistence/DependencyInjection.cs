using System;
using System.Linq;
using System.Reflection;
using AMP.Persistence.Database;
using AMP.Persistence.Repositories.UoW;
using AMP.Processors.ExceptionHandlers;
using AMP.Processors.Interfaces.UoW;
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
                // TODO:: switch db
                options.UseSqlServer(configuration.GetConnectionString("AmpProdDb"), opt =>
                {
                    opt.EnableRetryOnFailure();
                });
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

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            try
            {
                foreach (var repository in repositories)
                {
                    var iRepository = repository.GetInterfaces().FirstOrDefault(i => i.Name == $"I{repository.Name}") ??
                                      throw new RepositoryNotFoundException(
                                          $"{repository.Name} has no interface with name I{repository.Name}");
                    services.AddScoped(iRepository, repository);
                }
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