namespace AMP.WebApi.Installers;

public static class CorsInstaller
{
    public static IServiceCollection InstallCors(this IServiceCollection services)
    {
        services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:4200", "https://www.tukofix.com", "https://tukofix.com")
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });
        return services;
    }
}