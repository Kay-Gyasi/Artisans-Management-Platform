namespace AMP.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddAmp(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration)
            .AddRepositories()
            .AddProcessors()
            .RegisterAutoMapper()
            .AddMediatr();
        return services;
    }
}