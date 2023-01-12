using System.Reflection;
using AMP.Persistence.Repositories.Uow;
using AMP.Processors.Repositories.UoW;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ILogger = Serilog.ILogger;

namespace AMP.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            IConfiguration configuration,
            ILogger logger)
        {
            services.AddDbContext<AmpDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("AmpProdDb"),
                    opt =>
                {
                    opt.EnableRetryOnFailure();
                    opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });
            });
            services.AddScoped<IDapperContext, DapperContext>();

            services.AddRepositories(logger)
                .AddDbHealthChecks();
            return services;
        }

        /// <summary>
        ///    Registers all repositories in the assembly.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        /// <exception cref="RepositoryNotFoundException"></exception>
        private static IServiceCollection AddRepositories(this IServiceCollection services, Serilog.ILogger logger)
        {
            var definedTypes = typeof(DependencyInjection).Assembly.DefinedTypes;
            var repositories = definedTypes
                .Where(t => t.IsClass && t.GetCustomAttribute<RepositoryAttribute>() != null);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            try
            {
                foreach (var repository in repositories)
                {
                    var iRepository = repository.GetInterfaces()
                                          .FirstOrDefault(i => i.Name == $"I{repository.Name}") 
                                      ?? throw new RepositoryNotFoundException(
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
        
        private static IServiceCollection AddDbHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<AmpDbContext>();
            return services;
        }
    }
}